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
            var historyTicker = await _historyTickerService.GetAllByFileImportComplete(parametersFilter.FileImportId);

            var liqMedDiariaRemove = historyTicker.Where(obj => obj.AverageDailyLiquidity == null);
            historyTicker = historyTicker.Except(liqMedDiariaRemove)
                                         .ToList();

            if (parametersFilter.RemoveItemsWithNegativeValue)
            {
                var evEbitNegative = historyTicker.Where(obj => obj.EvEbit < 0);
                var precoLucroNegative = historyTicker.Where(obj => obj.PriceByProfit < 0);

                historyTicker = historyTicker.Except(evEbitNegative)
                                             .Except(precoLucroNegative)
                                             .ToList();
            }

            if (parametersFilter.RemoveItemsWithZeroValue)
            {
                var evEbitZero = historyTicker.Where(obj => obj.EvEbit == 0);
                var precoLucroZero = historyTicker.Where(obj => obj.PriceByProfit == 0);

                historyTicker = historyTicker.Except(evEbitZero)
                                             .Except(precoLucroZero)
                                             .ToList();
            }

            if (parametersFilter.MinimumLiquidity != null)
            {
                var itemsRemove = historyTicker.Where(obj => obj.AverageDailyLiquidity < parametersFilter.MinimumLiquidity);

                historyTicker = historyTicker.Except(itemsRemove)
                                             .ToList();
            }

            if (parametersFilter.MinimunEvEbit != null)
            {
                var itemsRemove = historyTicker.Where(obj => obj.EvEbit < parametersFilter.MinimunEvEbit);

                historyTicker = historyTicker.Except(itemsRemove)
                                             .ToList();
            }

            if (parametersFilter.MaximumEvEbit != null)
            {
                var itemsRemove = historyTicker.Where(obj => obj.EvEbit > parametersFilter.MaximumEvEbit);

                historyTicker = historyTicker.Except(itemsRemove)
                                             .ToList();
            }

            if (parametersFilter.MinimumPriceByProfit != null)
            {
                var itemsRemove = historyTicker.Where(obj => obj.PriceByProfit < parametersFilter.MinimumPriceByProfit);

                historyTicker = historyTicker.Except(itemsRemove)
                                             .ToList();
            }

            if (parametersFilter.MaximumPriceByProfit != null)
            {
                var itemsRemove = historyTicker.Where(obj => obj.PriceByProfit > parametersFilter.MaximumPriceByProfit);

                historyTicker = historyTicker.Except(itemsRemove)
                                             .ToList();
            }

            if (parametersFilter.MinimumEbitMargem != null)
            {
                var itemsRemove = historyTicker.Where(obj => obj.EbitMargin < parametersFilter.MinimumEbitMargem);

                historyTicker = historyTicker.Except(itemsRemove)
                                             .ToList();
            }

            if (parametersFilter.MaximumEbitMargem != null)
            {
                var itemsRemove = historyTicker.Where(obj => obj.EbitMargin > parametersFilter.MaximumEbitMargem);

                historyTicker = historyTicker.Except(itemsRemove)
                                             .ToList();
            }

            if (parametersFilter.RemoveItemsJudicialRecovery)
            {
                var itemsRemove = historyTicker.Where(obj => obj.Ticker.JudicialRecovery.Equals(true));

                historyTicker = historyTicker.Except(itemsRemove)
                                             .ToList();
            }

            var historyTickerOrdered = historyTicker.OrderBy(obj => obj.Ticker.BaseTicker).ThenByDescending(obj => obj.AverageDailyLiquidity);

            if (parametersFilter.RemoveLowerLiquidity)
            {
                string lastBaseTicker = string.Empty;
                IList<HistoryTickerDtoComplete> itemsRemove = new List<HistoryTickerDtoComplete>();

                foreach (var item in historyTickerOrdered)
                {
                    if (item.Ticker.BaseTicker.Equals(lastBaseTicker))
                    {
                        itemsRemove.Add(item);
                    }

                    lastBaseTicker = item.Ticker.BaseTicker;
                }

                historyTicker = historyTicker.Except(itemsRemove)
                                             .ToList();
            }

            return historyTicker;
        }

        private IOrderedEnumerable<FormulaDto> ReturnListOrderedGreenBlatt(IEnumerable<HistoryTickerDtoComplete> listTickers)
        {
            var historyTickerOrdered = listTickers.OrderBy(obj => obj.EvEbit);

            List<FormulaDto> listReturn = new List<FormulaDto>();
            int nPosition = 0;

            foreach (var item in historyTickerOrdered)
            {
                nPosition++;
                var ticker = new FormulaDto();

                ticker.BaseTicker = item.Ticker.BaseTicker;
                ticker.Ticker = item.Ticker.Ticker;
                ticker.Price = item.UnitPrice;
                ticker.DividendYield = Convert.ToDecimal(item.DividendYield);
                ticker.PriceByProfit = item.PriceByProfit;
                ticker.EvEbit = item.EvEbit;
                ticker.Roic = item.Roic;
                ticker.EbitMargin = item.EbitMargin;
                ticker.AverageDailyLiquidity = Convert.ToDecimal(item.AverageDailyLiquidity);
                ticker.JudicialRecovery = item.Ticker.JudicialRecovery;
                ticker.EvEbitScore = (listTickers.Count() - nPosition);

                listReturn.Add(ticker);
            }

            historyTickerOrdered = listTickers.OrderByDescending(obj => obj.Roic);
            nPosition = 0;

            foreach (var item in historyTickerOrdered)
            {
                nPosition++;
                var index = listReturn.FindIndex(0, listReturn.Count(), o => o.Ticker.Equals(item.Ticker.Ticker));
                listReturn[index].RoicScore = (listTickers.Count() - nPosition);
                listReturn[index].FinalScore = listReturn[index].RoicScore + listReturn[index].EvEbitScore;
            }

            return listReturn.OrderByDescending(obj => obj.FinalScore)
                             .ThenByDescending(obj => obj.AverageDailyLiquidity);
        }

        private IOrderedEnumerable<FormulaDto> ReturnListOrderedPriceAndProfit(IEnumerable<HistoryTickerDtoComplete> listTickers)
        {
            var historyTickerOrdered = listTickers.OrderBy(obj => obj.PriceByProfit);

            List<FormulaDto> listReturn = new List<FormulaDto>();
            int nPosition = 0;

            foreach (var item in historyTickerOrdered)
            {
                nPosition++;
                var ticker = new FormulaDto();

                ticker.BaseTicker = item.Ticker.BaseTicker;
                ticker.Ticker = item.Ticker.Ticker;
                ticker.Price = item.UnitPrice;
                ticker.DividendYield = Convert.ToDecimal(item.DividendYield);
                ticker.PriceByProfit = item.PriceByProfit;
                ticker.EvEbit = item.EvEbit;
                ticker.Roic = item.Roic;
                ticker.EbitMargin = item.EbitMargin;
                ticker.AverageDailyLiquidity = Convert.ToDecimal(item.AverageDailyLiquidity);
                ticker.JudicialRecovery = item.Ticker.JudicialRecovery;
                ticker.FinalScore = (listTickers.Count() - nPosition);

                listReturn.Add(ticker);
            }

            return listReturn.OrderByDescending(obj => obj.FinalScore);
        }

        private IOrderedEnumerable<FormulaDto> ReturnListValuetionByBazin(IEnumerable<HistoryTickerDtoComplete> listTickers)
        {
            var _dpaRemove = listTickers.Where(obj => obj.Dpa == 0 || obj.Dpa == null);

            listTickers = listTickers.Except(_dpaRemove)
                                     .ToList();

            List<FormulaDto> listReturn = new List<FormulaDto>();

            foreach (var item in listTickers)
            {
                var ticker = new FormulaDto();

                ticker.BaseTicker = item.Ticker.BaseTicker;
                ticker.Ticker = item.Ticker.Ticker;
                ticker.Price = item.UnitPrice;
                ticker.DividendYield = Convert.ToDecimal(item.DividendYield);
                ticker.PriceByProfit = item.PriceByProfit;
                ticker.EvEbit = item.EvEbit;
                ticker.Roic = item.Roic;
                ticker.EbitMargin = item.EbitMargin;
                ticker.AverageDailyLiquidity = Convert.ToDecimal(item.AverageDailyLiquidity);
                ticker.JudicialRecovery = item.Ticker.JudicialRecovery;

                decimal _justPrice = Convert.ToDecimal(item.Dpa) / Convert.ToDecimal(0.06);

                ticker.DiscountPercentage = ReturnDiscountPercentage(item.UnitPrice, _justPrice);

                listReturn.Add(ticker);
            }

            return listReturn.OrderBy(obj => obj.Ticker);
        }

        private IOrderedEnumerable<FormulaDto> ReturnListValuetionByGraham(IEnumerable<HistoryTickerDtoComplete> listTickers)
        {
            var _vpaRemove = listTickers.Where(obj => obj.Vpa <= 0);
            var _lpaRemove = listTickers.Where(obj => obj.Lpa <= 0);

            listTickers = listTickers.Except(_vpaRemove)
                                     .Except(_lpaRemove)
                                     .ToList();

            List<FormulaDto> listReturn = new List<FormulaDto>();

            foreach (var item in listTickers)
            {
                var ticker = new FormulaDto();

                ticker.BaseTicker = item.Ticker.BaseTicker;
                ticker.Ticker = item.Ticker.Ticker;
                ticker.Price = item.UnitPrice;
                ticker.DividendYield = Convert.ToDecimal(item.DividendYield);
                ticker.PriceByProfit = item.PriceByProfit;
                ticker.EvEbit = item.EvEbit;
                ticker.Roic = item.Roic;
                ticker.EbitMargin = item.EbitMargin;
                ticker.AverageDailyLiquidity = Convert.ToDecimal(item.AverageDailyLiquidity);
                ticker.JudicialRecovery = item.Ticker.JudicialRecovery;

                var valor = Convert.ToDecimal(22.5) * item.Lpa * item.Vpa;
                var isNegativo = (valor < 0 ? true : false);

                valor = (isNegativo ? valor * -1 : valor * 1);

                decimal _justPrice = Convert.ToDecimal(Math.Sqrt(Convert.ToDouble(valor)));
                _justPrice = (isNegativo ? _justPrice * -1 : _justPrice * 1);

                ticker.DiscountPercentage = ReturnDiscountPercentage(item.UnitPrice, _justPrice);

                listReturn.Add(ticker);
            }

            return listReturn.OrderBy(obj => obj.Ticker);
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

            List<FormulaDto> listReturn = new List<FormulaDto>();

            foreach (var item in listTickers)
            {
                var ticker = new FormulaDto();

                // update to value for Zero, case necessary
                item.ProfitCAGR = Convert.ToDecimal(item.ProfitCAGR);

                ticker.BaseTicker = item.Ticker.BaseTicker;
                ticker.Ticker = item.Ticker.Ticker;
                ticker.Price = item.UnitPrice;
                ticker.DividendYield = Convert.ToDecimal(item.DividendYield);
                ticker.PriceByProfit = item.PriceByProfit;
                ticker.EvEbit = item.EvEbit;
                ticker.Roic = item.Roic;
                ticker.EbitMargin = item.EbitMargin;
                ticker.AverageDailyLiquidity = Convert.ToDecimal(item.AverageDailyLiquidity);
                ticker.JudicialRecovery = item.Ticker.JudicialRecovery;

                // 1 - reference a 1 year in future
                decimal _valueFutureDy = Convert.ToDecimal(item.ProfitCAGR != 0 ? item.Dpa * (1 + (item.ProfitCAGR / 100)) : item.Dpa);
                decimal _justPrice = Convert.ToDecimal(_valueFutureDy / ((marketRisk - item.ProfitCAGR) / 100));

                ticker.DiscountPercentage = ReturnDiscountPercentage(item.UnitPrice, _justPrice);

                listReturn.Add(ticker);
            }

            return listReturn.OrderBy(obj => obj.Ticker);
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
