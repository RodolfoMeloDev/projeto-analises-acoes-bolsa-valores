using App.Domain.Dtos.Formula;
using App.Domain.Dtos.HistoryTicker;
using App.Domain.Interfaces.Services.Formula;
using App.Domain.Interfaces.Services.HistoryTicker;
using App.Service.Services.Exceptions;

namespace App.Service.Services
{
    public class FormulaService : IFormulaService
    {
        private IHistoryTickerService _historyTickerService;

        public FormulaService(IHistoryTickerService historyTickerService)
        {
            _historyTickerService = historyTickerService;
        }

        private decimal ReturnDiscountPercentage(decimal price, decimal justPrice)
        {

            if (justPrice == 0)
                throw new FormulaException("O preço justo não pode ser ZERO.");

            var difPrice = justPrice - price;

            return decimal.Round((difPrice / justPrice) * 100, 2);
        }

        private async Task<IEnumerable<HistoryTickerDtoComplete>> ReturnListWithParametersExecuted(ParametersFilter parametersFilter)
        {
            var _historyTicker = await _historyTickerService.GetAllByFileImportComplete(parametersFilter.FileImportId);

            var averageDailyLiquidityRemove = _historyTicker.Where(obj => obj.AverageDailyLiquidity == null);
            _historyTicker = _historyTicker.Except(averageDailyLiquidityRemove)
                                         .ToList();

            if (parametersFilter.RemoveItemsWithNegativeValue)
            {
                var _evEbitNegative = _historyTicker.Where(obj => obj.EvEbit < 0);
                var _priceByProfitNegative = _historyTicker.Where(obj => obj.PriceByProfit < 0);

                _historyTicker = _historyTicker.Except(_evEbitNegative)
                                             .Except(_priceByProfitNegative)
                                             .ToList();
            }

            if (parametersFilter.RemoveItemsWithZeroValue)
            {
                var _evEbitZeroRemove = _historyTicker.Where(obj => obj.EvEbit == 0);
                var _priceByProfitNegativeRemove = _historyTicker.Where(obj => obj.PriceByProfit == 0);

                _historyTicker = _historyTicker.Except(_evEbitZeroRemove)
                                             .Except(_priceByProfitNegativeRemove)
                                             .ToList();
            }

            if (parametersFilter.MinimumLiquidity != null)
            {
                var _itemsRemove = _historyTicker.Where(obj => obj.AverageDailyLiquidity < parametersFilter.MinimumLiquidity);

                _historyTicker = _historyTicker.Except(_itemsRemove)
                                             .ToList();
            }

            if (parametersFilter.MinimunEvEbit != null)
            {
                var _itemsRemove = _historyTicker.Where(obj => obj.EvEbit < parametersFilter.MinimunEvEbit);

                _historyTicker = _historyTicker.Except(_itemsRemove)
                                             .ToList();
            }

            if (parametersFilter.MaximumEvEbit != null)
            {
                var _itemsRemove = _historyTicker.Where(obj => obj.EvEbit > parametersFilter.MaximumEvEbit);

                _historyTicker = _historyTicker.Except(_itemsRemove)
                                             .ToList();
            }

            if (parametersFilter.MinimumPriceByProfit != null)
            {
                var _itemsRemove = _historyTicker.Where(obj => obj.PriceByProfit < parametersFilter.MinimumPriceByProfit);

                _historyTicker = _historyTicker.Except(_itemsRemove)
                                             .ToList();
            }

            if (parametersFilter.MaximumPriceByProfit != null)
            {
                var _itemsRemove = _historyTicker.Where(obj => obj.PriceByProfit > parametersFilter.MaximumPriceByProfit);

                _historyTicker = _historyTicker.Except(_itemsRemove)
                                             .ToList();
            }

            if (parametersFilter.MinimumEbitMargem != null)
            {
                var _itemsRemove = _historyTicker.Where(obj => obj.EbitMargin < parametersFilter.MinimumEbitMargem);

                _historyTicker = _historyTicker.Except(_itemsRemove)
                                             .ToList();
            }

            if (parametersFilter.MaximumEbitMargem != null)
            {
                var _itemsRemove = _historyTicker.Where(obj => obj.EbitMargin > parametersFilter.MaximumEbitMargem);

                _historyTicker = _historyTicker.Except(_itemsRemove)
                                             .ToList();
            }

            if (parametersFilter.RemoveItemsJudicialRecovery)
            {
                var _itemsRemove = _historyTicker.Where(obj => obj.Ticker.JudicialRecovery.Equals(true));

                _historyTicker = _historyTicker.Except(_itemsRemove)
                                             .ToList();
            }

            var _historyTickerOrdered = _historyTicker.OrderBy(obj => obj.Ticker.BaseTicker).ThenByDescending(obj => obj.AverageDailyLiquidity);

            if (parametersFilter.RemoveLowerLiquidity)
            {
                string _lastBaseTicker = string.Empty;
                IList<HistoryTickerDtoComplete> _itemsRemove = new List<HistoryTickerDtoComplete>();

                foreach (var item in _historyTickerOrdered)
                {
                    if (item.Ticker.BaseTicker.Equals(_lastBaseTicker))
                    {
                        _itemsRemove.Add(item);
                    }

                    _lastBaseTicker = item.Ticker.BaseTicker;
                }

                _historyTicker = _historyTicker.Except(_itemsRemove)
                                             .ToList();
            }

            return _historyTicker;
        }

        private IOrderedEnumerable<FormulaDto> ReturnListOrderedGreenBlatt(IEnumerable<HistoryTickerDtoComplete> listTickers)
        {
            var _historyTickerOrdered = listTickers.OrderBy(obj => obj.EvEbit);

            List<FormulaDto> _listReturn = new List<FormulaDto>();
            int _Position = 0;

            foreach (var item in _historyTickerOrdered)
            {
                _Position++;
                var _ticker = new FormulaDto();

                _ticker.BaseTicker = item.Ticker.BaseTicker;
                _ticker.Ticker = item.Ticker.Ticker;
                _ticker.Price = item.UnitPrice;
                _ticker.DividendYield = Convert.ToDecimal(item.DividendYield);
                _ticker.PriceByProfit = item.PriceByProfit;
                _ticker.EvEbit = item.EvEbit;
                _ticker.Roic = item.Roic;
                _ticker.EbitMargin = item.EbitMargin;
                _ticker.AverageDailyLiquidity = Convert.ToDecimal(item.AverageDailyLiquidity);
                _ticker.JudicialRecovery = item.Ticker.JudicialRecovery;
                _ticker.EvEbitScore = (listTickers.Count() - _Position);

                _listReturn.Add(_ticker);
            }

            _historyTickerOrdered = listTickers.OrderByDescending(obj => obj.Roic);
            _Position = 0;

            foreach (var item in _historyTickerOrdered)
            {
                _Position++;
                var index = _listReturn.FindIndex(0, _listReturn.Count(), o => o.Ticker.Equals(item.Ticker.Ticker));
                _listReturn[index].RoicScore = (listTickers.Count() - _Position);
                _listReturn[index].FinalScore = _listReturn[index].RoicScore + _listReturn[index].EvEbitScore;
            }

            return _listReturn.OrderByDescending(obj => obj.FinalScore)
                             .ThenByDescending(obj => obj.AverageDailyLiquidity);
        }

        private IOrderedEnumerable<FormulaDto> ReturnListOrderedPriceAndProfit(IEnumerable<HistoryTickerDtoComplete> listTickers)
        {
            var _historyTickerOrdered = listTickers.OrderBy(obj => obj.PriceByProfit);

            List<FormulaDto> _listReturn = new List<FormulaDto>();
            int _Position = 0;

            foreach (var item in _historyTickerOrdered)
            {
                _Position++;
                var _ticker = new FormulaDto();

                _ticker.BaseTicker = item.Ticker.BaseTicker;
                _ticker.Ticker = item.Ticker.Ticker;
                _ticker.Price = item.UnitPrice;
                _ticker.DividendYield = Convert.ToDecimal(item.DividendYield);
                _ticker.PriceByProfit = item.PriceByProfit;
                _ticker.EvEbit = item.EvEbit;
                _ticker.Roic = item.Roic;
                _ticker.EbitMargin = item.EbitMargin;
                _ticker.AverageDailyLiquidity = Convert.ToDecimal(item.AverageDailyLiquidity);
                _ticker.JudicialRecovery = item.Ticker.JudicialRecovery;
                _ticker.FinalScore = (listTickers.Count() - _Position);

                _listReturn.Add(_ticker);
            }

            return _listReturn.OrderByDescending(obj => obj.PriceByProfit);
        }

        private IOrderedEnumerable<FormulaDto> ReturnListValuetionByBazin(IEnumerable<HistoryTickerDtoComplete> listTickers)
        {
            var _dpaRemove = listTickers.Where(obj => obj.Dpa == 0 || obj.Dpa == null);

            listTickers = listTickers.Except(_dpaRemove)
                                     .ToList();

            List<FormulaDto> _listReturn = new List<FormulaDto>();

            foreach (var item in listTickers)
            {
                var _ticker = new FormulaDto();

                _ticker.BaseTicker = item.Ticker.BaseTicker;
                _ticker.Ticker = item.Ticker.Ticker;
                _ticker.Price = item.UnitPrice;
                _ticker.DividendYield = Convert.ToDecimal(item.DividendYield);
                _ticker.PriceByProfit = item.PriceByProfit;
                _ticker.EvEbit = item.EvEbit;
                _ticker.Roic = item.Roic;
                _ticker.EbitMargin = item.EbitMargin;
                _ticker.AverageDailyLiquidity = Convert.ToDecimal(item.AverageDailyLiquidity);
                _ticker.JudicialRecovery = item.Ticker.JudicialRecovery;

                decimal _justPrice = Convert.ToDecimal(item.Dpa) / Convert.ToDecimal(0.06);

                _ticker.DiscountPercentage = ReturnDiscountPercentage(item.UnitPrice, _justPrice);

                _listReturn.Add(_ticker);
            }

            return _listReturn.OrderBy(obj => obj.Ticker);
        }

        private IOrderedEnumerable<FormulaDto> ReturnListValuetionByGraham(IEnumerable<HistoryTickerDtoComplete> listTickers)
        {
            var _vpaRemove = listTickers.Where(obj => obj.Vpa <= 0);
            var _lpaRemove = listTickers.Where(obj => obj.Lpa <= 0);

            listTickers = listTickers.Except(_vpaRemove)
                                     .Except(_lpaRemove)
                                     .ToList();

            List<FormulaDto> _listReturn = new List<FormulaDto>();

            foreach (var item in listTickers)
            {
                var _ticker = new FormulaDto();

                _ticker.BaseTicker = item.Ticker.BaseTicker;
                _ticker.Ticker = item.Ticker.Ticker;
                _ticker.Price = item.UnitPrice;
                _ticker.DividendYield = Convert.ToDecimal(item.DividendYield);
                _ticker.PriceByProfit = item.PriceByProfit;
                _ticker.EvEbit = item.EvEbit;
                _ticker.Roic = item.Roic;
                _ticker.EbitMargin = item.EbitMargin;
                _ticker.AverageDailyLiquidity = Convert.ToDecimal(item.AverageDailyLiquidity);
                _ticker.JudicialRecovery = item.Ticker.JudicialRecovery;

                var valor = Convert.ToDecimal(22.5) * item.Lpa * item.Vpa;
                var isNegativo = (valor < 0 ? true : false);

                valor = (isNegativo ? valor * -1 : valor * 1);

                decimal _justPrice = Convert.ToDecimal(Math.Sqrt(Convert.ToDouble(valor)));
                _justPrice = (isNegativo ? _justPrice * -1 : _justPrice * 1);

                _ticker.DiscountPercentage = ReturnDiscountPercentage(item.UnitPrice, _justPrice);

                _listReturn.Add(_ticker);
            }

            return _listReturn.OrderBy(obj => obj.Ticker);
        }

        private IOrderedEnumerable<FormulaDto> ReturnListValuetionByGordon(IEnumerable<HistoryTickerDtoComplete> listTickers, decimal marketRisk)
        {
            var _dyRemove = listTickers.Where(obj => obj.DividendYield <= 0 || obj.DividendYield == null);
            var _dpaRemove = listTickers.Where(obj => obj.Dpa == null);
            var _cagrProfitRemove = listTickers.Where(obj => obj.ProfitCAGR < 0);

            listTickers = listTickers.Except(_dyRemove)
                                     .Except(_dpaRemove)
                                     .Except(_cagrProfitRemove)
                                     .ToList();

            List<FormulaDto> _listReturn = new List<FormulaDto>();

            foreach (var item in listTickers)
            {
                var _ticker = new FormulaDto();

                // update to value for Zero, case necessary
                item.ProfitCAGR = Convert.ToDecimal(item.ProfitCAGR);

                _ticker.BaseTicker = item.Ticker.BaseTicker;
                _ticker.Ticker = item.Ticker.Ticker;
                _ticker.Price = item.UnitPrice;
                _ticker.DividendYield = Convert.ToDecimal(item.DividendYield);
                _ticker.PriceByProfit = item.PriceByProfit;
                _ticker.EvEbit = item.EvEbit;
                _ticker.Roic = item.Roic;
                _ticker.EbitMargin = item.EbitMargin;
                _ticker.AverageDailyLiquidity = Convert.ToDecimal(item.AverageDailyLiquidity);
                _ticker.JudicialRecovery = item.Ticker.JudicialRecovery;

                // 1 - reference a 1 year in future
                decimal _valueFutureDy = Convert.ToDecimal(item.ProfitCAGR != 0 ? item.Dpa * (1 + (item.ProfitCAGR / 100)) : item.Dpa);
                decimal _justPrice = Convert.ToDecimal(_valueFutureDy / ((marketRisk - item.ProfitCAGR) / 100));

                _ticker.DiscountPercentage = ReturnDiscountPercentage(item.UnitPrice, _justPrice);

                _listReturn.Add(_ticker);
            }

            return _listReturn.OrderBy(obj => obj.Ticker);
        }

        public async Task<IEnumerable<FormulaDto>> Greenblatt(ParametersFilter parametersFilter)
        {
            return ReturnListOrderedGreenBlatt(await ReturnListWithParametersExecuted(parametersFilter));
        }

        public async Task<IEnumerable<FormulaDto>> PriceAndProfit(ParametersFilter parametersFilter)
        {
            return ReturnListOrderedPriceAndProfit(await ReturnListWithParametersExecuted(parametersFilter));
        }

        public async Task<IEnumerable<FormulaDto>> ValuetionByBazin(ParametersFilter parametersFilter)
        {
            return ReturnListValuetionByBazin(await ReturnListWithParametersExecuted(parametersFilter));
        }

        public async Task<IEnumerable<FormulaDto>> ValuetionByGraham(ParametersFilter parametersFilter)
        {
            return ReturnListValuetionByGraham(await ReturnListWithParametersExecuted(parametersFilter));
        }

        public async Task<IEnumerable<FormulaDto>> ValuetionByGordon(ParametersFilter parametersFilter)
        {
            if (parametersFilter.MarketRisk == null || parametersFilter.MarketRisk <= 0)
                throw new FormulaException("Deve ser informado um valor válido, acima de ZERO, para o parametro Risco de Mercado");

            return ReturnListValuetionByGordon(await ReturnListWithParametersExecuted(parametersFilter), Convert.ToDecimal(parametersFilter.MarketRisk));
        }
    }
}
