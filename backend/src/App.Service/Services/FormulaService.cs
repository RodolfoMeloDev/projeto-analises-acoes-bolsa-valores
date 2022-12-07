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

            var _historyTickerOrdered = _historyTicker.OrderBy(obj => obj.Ticker.BaseTicker.BaseTicker).ThenByDescending(obj => obj.AverageDailyLiquidity);

            if (parametersFilter.RemoveLowerLiquidity)
            {
                string _lastBaseTicker = string.Empty;
                IList<HistoryTickerDtoComplete> _itemsRemove = new List<HistoryTickerDtoComplete>();

                foreach (var item in _historyTickerOrdered)
                {
                    if (item.Ticker.BaseTicker.BaseTicker.Equals(_lastBaseTicker))
                    {
                        _itemsRemove.Add(item);
                    }

                    _lastBaseTicker = item.Ticker.BaseTicker.BaseTicker;
                }

                _historyTicker = _historyTicker.Except(_itemsRemove)
                                               .ToList();
            }

            return _historyTicker;
        }

        private IOrderedEnumerable<FormulaDtoGreenBlatt> ReturnListOrderedGreenBlatt(IEnumerable<HistoryTickerDtoComplete> listTickers)
        {
            var _tickersRemove = listTickers.Where(obj => obj.Ticker.Ticker.Contains("33"));

            listTickers = listTickers.Except(_tickersRemove)
                                     .ToList();

            var _historyTickerOrdered = listTickers.OrderBy(obj => obj.EvEbit);

            List<FormulaDtoGreenBlatt> _listScore = new List<FormulaDtoGreenBlatt>();
            int _Position = 0;

            foreach (var item in _historyTickerOrdered)
            {

                _Position++;
                var _ticker = new FormulaDtoGreenBlatt();

                _ticker.NameSeguiment = item.Ticker.BaseTicker.Segment.Name;
                _ticker.Ticker = item.Ticker.Ticker;
                _ticker.Price = item.UnitPrice;
                _ticker.DividendYield = Convert.ToDecimal(item.DividendYield);
                _ticker.PriceByProfit = item.PriceByProfit;
                _ticker.Lpa = item.Lpa;
                _ticker.Vpa = item.Vpa;
                _ticker.Dpa = (item.Dpa == null ? null : decimal.Round(Convert.ToDecimal(item.Dpa), 2));
                _ticker.Payout = (item.Payout == null ? null : decimal.Round(Convert.ToDecimal(item.Payout), 2));
                _ticker.Roe = item.Roe;
                _ticker.Roic = item.Roic;
                _ticker.EvEbit = item.EvEbit;
                _ticker.EbitMargin = item.EbitMargin;
                _ticker.ProfitCAGR = item.ProfitCAGR;
                _ticker.ExpectedGrowth = decimal.Round(item.ExpectedGrowth, 2);
                _ticker.AverageGrowth = decimal.Round(item.AverageGrowth, 2);
                _ticker.AverageDailyLiquidity = Convert.ToDecimal(item.AverageDailyLiquidity);
                _ticker.JudicialRecovery = item.Ticker.JudicialRecovery;
                _ticker.EvEbitScore = (listTickers.Count() - _Position);

                _listScore.Add(_ticker);
            }

            _historyTickerOrdered = listTickers.OrderByDescending(obj => obj.Roic);
            _Position = 0;

            foreach (var item in _historyTickerOrdered)
            {
                _Position++;
                var index = _listScore.FindIndex(0, _listScore.Count(), o => o.Ticker.Equals(item.Ticker.Ticker));
                _listScore[index].RoicScore = (listTickers.Count() - _Position);
                _listScore[index].FinalScore = _listScore[index].RoicScore + _listScore[index].EvEbitScore;
            }

            var _listReturn = _listScore.OrderByDescending(obj => obj.FinalScore)
                                        .ThenByDescending(obj => obj.AverageDailyLiquidity);
            _Position = 0;
            foreach (var item in _listReturn)
            {
                _Position++;
                item.Position = _Position;
            }

            return _listReturn.OrderBy(obj => obj.Position);
        }

        private IOrderedEnumerable<FormulaDtoPriceAndProfit> ReturnListOrderedPriceAndProfit(IEnumerable<HistoryTickerDtoComplete> listTickers)
        {
            var _historyTickerOrdered = listTickers.OrderBy(obj => obj.PriceByProfit);

            List<FormulaDtoPriceAndProfit> _listScore = new List<FormulaDtoPriceAndProfit>();
            int _Position = 0;

            foreach (var item in _historyTickerOrdered)
            {
                _Position++;
                var _ticker = new FormulaDtoPriceAndProfit();

                _ticker.Position = _Position;
                _ticker.NameSeguiment = item.Ticker.BaseTicker.Segment.Name;
                _ticker.Ticker = item.Ticker.Ticker;
                _ticker.Price = item.UnitPrice;
                _ticker.DividendYield = Convert.ToDecimal(item.DividendYield);
                _ticker.PriceByProfit = item.PriceByProfit;
                _ticker.Lpa = item.Lpa;
                _ticker.Vpa = item.Vpa;
                _ticker.Dpa = (item.Dpa == null ? null : decimal.Round(Convert.ToDecimal(item.Dpa), 2));
                _ticker.Payout = (item.Payout == null ? null : decimal.Round(Convert.ToDecimal(item.Payout), 2));
                _ticker.Roe = item.Roe;
                _ticker.Roic = item.Roic;
                _ticker.EvEbit = item.EvEbit;
                _ticker.EbitMargin = item.EbitMargin;
                _ticker.ProfitCAGR = item.ProfitCAGR;
                _ticker.ExpectedGrowth = decimal.Round(item.ExpectedGrowth, 2);
                _ticker.AverageGrowth = decimal.Round(item.AverageGrowth, 2);
                _ticker.AverageDailyLiquidity = Convert.ToDecimal(item.AverageDailyLiquidity);
                _ticker.JudicialRecovery = item.Ticker.JudicialRecovery;

                _listScore.Add(_ticker);
            }

            return _listScore.OrderBy(obj => obj.PriceByProfit);
        }

        private IOrderedEnumerable<FormulaDtoEvEbit> ReturnListOrderedEvEbit(IEnumerable<HistoryTickerDtoComplete> listTickers)
        {
            var _historyTickerOrdered = listTickers.OrderBy(obj => obj.EvEbit);

            List<FormulaDtoEvEbit> _listScore = new List<FormulaDtoEvEbit>();
            int _Position = 0;

            foreach (var item in _historyTickerOrdered)
            {
                _Position++;
                var _ticker = new FormulaDtoEvEbit();

                _ticker.Position = _Position;
                _ticker.NameSeguiment = item.Ticker.BaseTicker.Segment.Name;
                _ticker.Ticker = item.Ticker.Ticker;
                _ticker.Price = item.UnitPrice;
                _ticker.DividendYield = Convert.ToDecimal(item.DividendYield);
                _ticker.PriceByProfit = item.PriceByProfit;
                _ticker.Lpa = item.Lpa;
                _ticker.Vpa = item.Vpa;
                _ticker.Dpa = (item.Dpa == null ? null : decimal.Round(Convert.ToDecimal(item.Dpa), 2));
                _ticker.Payout = (item.Payout == null ? null : decimal.Round(Convert.ToDecimal(item.Payout), 2));
                _ticker.Roe = item.Roe;
                _ticker.Roic = item.Roic;
                _ticker.EvEbit = item.EvEbit;
                _ticker.EbitMargin = item.EbitMargin;
                _ticker.ProfitCAGR = item.ProfitCAGR;
                _ticker.ExpectedGrowth = decimal.Round(item.ExpectedGrowth, 2);
                _ticker.AverageGrowth = decimal.Round(item.AverageGrowth, 2);
                _ticker.AverageDailyLiquidity = Convert.ToDecimal(item.AverageDailyLiquidity);
                _ticker.JudicialRecovery = item.Ticker.JudicialRecovery;

                _listScore.Add(_ticker);
            }

            return _listScore.OrderBy(obj => obj.Position);
        }

        private IOrderedEnumerable<FormulaDtoBazin> ReturnListValuetionByBazin(IEnumerable<HistoryTickerDtoComplete> listTickers)
        {
            var _dpaRemove = listTickers.Where(obj => obj.Dpa == 0 || obj.Dpa == null);

            listTickers = listTickers.Except(_dpaRemove)
                                     .ToList();

            List<FormulaDtoBazin> _listScore = new List<FormulaDtoBazin>();

            foreach (var item in listTickers)
            {
                var _ticker = new FormulaDtoBazin();

                _ticker.NameSeguiment = item.Ticker.BaseTicker.Segment.Name;
                _ticker.Ticker = item.Ticker.Ticker;
                _ticker.Price = item.UnitPrice;
                _ticker.DividendYield = Convert.ToDecimal(item.DividendYield);
                _ticker.PriceByProfit = item.PriceByProfit;
                _ticker.Lpa = item.Lpa;
                _ticker.Vpa = item.Vpa;
                _ticker.Dpa = (item.Dpa == null ? null : decimal.Round(Convert.ToDecimal(item.Dpa), 2));
                _ticker.Payout = (item.Payout == null ? null : decimal.Round(Convert.ToDecimal(item.Payout), 2));
                _ticker.Roe = item.Roe;
                _ticker.Roic = item.Roic;
                _ticker.EvEbit = item.EvEbit;
                _ticker.EbitMargin = item.EbitMargin;
                _ticker.ProfitCAGR = item.ProfitCAGR;
                _ticker.ExpectedGrowth = decimal.Round(item.ExpectedGrowth, 2);
                _ticker.AverageGrowth = decimal.Round(item.AverageGrowth, 2);
                _ticker.AverageDailyLiquidity = Convert.ToDecimal(item.AverageDailyLiquidity);
                _ticker.JudicialRecovery = item.Ticker.JudicialRecovery;

                decimal _justPrice = Convert.ToDecimal(item.Dpa) / Convert.ToDecimal(0.06);

                _ticker.JustPrice = decimal.Round(_justPrice, 2);
                _ticker.DiscountPercentage = ReturnDiscountPercentage(item.UnitPrice, _justPrice);

                _listScore.Add(_ticker);
            }

            return _listScore.OrderBy(obj => obj.Ticker);
        }

        private IOrderedEnumerable<FormulaDtoGraham> ReturnListValuetionByGraham(IEnumerable<HistoryTickerDtoComplete> listTickers)
        {
            var _vpaRemove = listTickers.Where(obj => obj.Vpa <= 0);
            var _lpaRemove = listTickers.Where(obj => obj.Lpa <= 0);

            listTickers = listTickers.Except(_vpaRemove)
                                     .Except(_lpaRemove)
                                     .ToList();

            List<FormulaDtoGraham> _listScore = new List<FormulaDtoGraham>();

            foreach (var item in listTickers)
            {
                var _ticker = new FormulaDtoGraham();

                _ticker.NameSeguiment = item.Ticker.BaseTicker.Segment.Name;
                _ticker.Ticker = item.Ticker.Ticker;
                _ticker.Price = item.UnitPrice;
                _ticker.DividendYield = Convert.ToDecimal(item.DividendYield);
                _ticker.PriceByProfit = item.PriceByProfit;
                _ticker.Lpa = item.Lpa;
                _ticker.Vpa = item.Vpa;
                _ticker.Dpa = (item.Dpa == null ? null : decimal.Round(Convert.ToDecimal(item.Dpa), 2));
                _ticker.Payout = (item.Payout == null ? null : decimal.Round(Convert.ToDecimal(item.Payout), 2));
                _ticker.Roe = item.Roe;
                _ticker.Roic = item.Roic;
                _ticker.EvEbit = item.EvEbit;
                _ticker.EbitMargin = item.EbitMargin;
                _ticker.ProfitCAGR = item.ProfitCAGR;
                _ticker.ExpectedGrowth = decimal.Round(item.ExpectedGrowth, 2);
                _ticker.AverageGrowth = decimal.Round(item.AverageGrowth, 2);
                _ticker.AverageDailyLiquidity = Convert.ToDecimal(item.AverageDailyLiquidity);
                _ticker.JudicialRecovery = item.Ticker.JudicialRecovery;

                var valor = Convert.ToDecimal(22.5) * item.Lpa * item.Vpa;
                var isNegativo = (valor < 0 ? true : false);

                valor = (isNegativo ? valor * -1 : valor * 1);

                decimal _justPrice = Convert.ToDecimal(Math.Sqrt(Convert.ToDouble(valor)));
                _justPrice = (isNegativo ? _justPrice * -1 : _justPrice * 1);

                _ticker.JustPrice = decimal.Round(_justPrice, 2);
                _ticker.DiscountPercentage = ReturnDiscountPercentage(item.UnitPrice, _justPrice);

                _listScore.Add(_ticker);
            }

            return _listScore.OrderBy(obj => obj.Ticker);
        }

        private IOrderedEnumerable<FormulaDtoGordon> ReturnListValuetionByGordon(IEnumerable<HistoryTickerDtoComplete> listTickers, decimal marketRisk)
        {
            var _dyRemove = listTickers.Where(obj => obj.DividendYield <= 0 || obj.DividendYield == null);
            var _dpaRemove = listTickers.Where(obj => obj.Dpa == null);
            var _cagrProfitRemove = listTickers.Where(obj => obj.ProfitCAGR < 0);

            listTickers = listTickers.Except(_dyRemove)
                                     .Except(_dpaRemove)
                                     .Except(_cagrProfitRemove)
                                     .ToList();

            List<FormulaDtoGordon> _listScore = new List<FormulaDtoGordon>();

            foreach (var item in listTickers)
            {
                var _ticker = new FormulaDtoGordon();

                // update to value for Zero, case necessary
                item.ProfitCAGR = Convert.ToDecimal(item.ProfitCAGR);

                _ticker.NameSeguiment = item.Ticker.BaseTicker.Segment.Name;
                _ticker.Ticker = item.Ticker.Ticker;
                _ticker.Price = item.UnitPrice;
                _ticker.DividendYield = Convert.ToDecimal(item.DividendYield);
                _ticker.PriceByProfit = item.PriceByProfit;
                _ticker.Lpa = item.Lpa;
                _ticker.Vpa = item.Vpa;
                _ticker.Dpa = (item.Dpa == null ? null : decimal.Round(Convert.ToDecimal(item.Dpa), 2));
                _ticker.Payout = (item.Payout == null ? null : decimal.Round(Convert.ToDecimal(item.Payout), 2));
                _ticker.Roe = item.Roe;
                _ticker.Roic = item.Roic;
                _ticker.EvEbit = item.EvEbit;
                _ticker.EbitMargin = item.EbitMargin;
                _ticker.ProfitCAGR = item.ProfitCAGR;
                _ticker.ExpectedGrowth = decimal.Round(item.ExpectedGrowth, 2);
                _ticker.AverageGrowth = decimal.Round(item.AverageGrowth, 2);
                _ticker.AverageDailyLiquidity = Convert.ToDecimal(item.AverageDailyLiquidity);
                _ticker.JudicialRecovery = item.Ticker.JudicialRecovery;

                // 1 - reference a 1 year in future
                decimal _valueFutureDy = Convert.ToDecimal(item.ProfitCAGR != 0 ? item.Dpa * (1 + (item.ProfitCAGR / 100)) : item.Dpa);
                decimal _justPrice = Convert.ToDecimal(_valueFutureDy / ((marketRisk - item.ProfitCAGR) / 100));

                _ticker.JustPrice = decimal.Round(_justPrice, 2);
                _ticker.DiscountPercentage = ReturnDiscountPercentage(item.UnitPrice, _justPrice);

                _ticker.MarketRisk = marketRisk;

                _listScore.Add(_ticker);
            }

            return _listScore.OrderBy(obj => obj.Ticker);
        }

        public async Task<IEnumerable<FormulaDtoGreenBlatt>> Greenblatt(ParametersFilter parametersFilter)
        {
            return ReturnListOrderedGreenBlatt(await ReturnListWithParametersExecuted(parametersFilter));
        }

        public async Task<IEnumerable<FormulaDtoPriceAndProfit>> PriceAndProfit(ParametersFilter parametersFilter)
        {
            return ReturnListOrderedPriceAndProfit(await ReturnListWithParametersExecuted(parametersFilter));
        }

        public async Task<IEnumerable<FormulaDtoBazin>> ValuetionByBazin(ParametersFilter parametersFilter)
        {
            return ReturnListValuetionByBazin(await ReturnListWithParametersExecuted(parametersFilter));
        }

        public async Task<IEnumerable<FormulaDtoGraham>> ValuetionByGraham(ParametersFilter parametersFilter)
        {
            return ReturnListValuetionByGraham(await ReturnListWithParametersExecuted(parametersFilter));
        }

        public async Task<IEnumerable<FormulaDtoGordon>> ValuetionByGordon(ParametersFilter parametersFilter)
        {
            if (parametersFilter.MarketRisk == null || parametersFilter.MarketRisk <= 0)
                throw new FormulaException("Deve ser informado um valor válido, acima de ZERO, para o parametro Risco de Mercado");

            return ReturnListValuetionByGordon(await ReturnListWithParametersExecuted(parametersFilter), Convert.ToDecimal(parametersFilter.MarketRisk));
        }

        public async Task<IEnumerable<FormulaDtoEvEbit>> EvEbit(ParametersFilter parametersFilter)
        {
            return ReturnListOrderedEvEbit(await ReturnListWithParametersExecuted(parametersFilter));
        }

        public async Task<FormulaDtoPosition> TickersAnalisys(ParametersFilter parametersFilter)
        {
            if (parametersFilter.MarketRisk == null || parametersFilter.MarketRisk <= 0)
                throw new FormulaException("Deve ser informado um valor válido, acima de ZERO, para o parametro Risco de Mercado");

            if (string.IsNullOrEmpty(parametersFilter.Ticker))
                throw new FormulaException("Deve ser informado o Ticker!");

            var tickerGreenBlatt = ReturnListOrderedGreenBlatt(await ReturnListWithParametersExecuted(parametersFilter)).Where(x => x.Ticker.Equals(parametersFilter.Ticker.ToUpper()))
                                                                                                                        .FirstOrDefault();
            var tickerPriceAndProfit = ReturnListOrderedPriceAndProfit(await ReturnListWithParametersExecuted(parametersFilter)).Where(x => x.Ticker.Equals(parametersFilter.Ticker.ToUpper()))
                                                                                                                                .FirstOrDefault();
            var tickerEvEbit = ReturnListOrderedEvEbit(await ReturnListWithParametersExecuted(parametersFilter)).Where(x => x.Ticker.Equals(parametersFilter.Ticker.ToUpper()))
                                                                                                                .FirstOrDefault();
            var tickerBazin = ReturnListValuetionByBazin(await ReturnListWithParametersExecuted(parametersFilter)).Where(x => x.Ticker.Equals(parametersFilter.Ticker.ToUpper()))
                                                                                                                  .FirstOrDefault();
            var tickerGraham = ReturnListValuetionByGraham(await ReturnListWithParametersExecuted(parametersFilter)).Where(x => x.Ticker.Equals(parametersFilter.Ticker.ToUpper()))
                                                                                                                    .FirstOrDefault();
            var tickerGordon = ReturnListValuetionByGordon(await ReturnListWithParametersExecuted(parametersFilter),
                                                           Convert.ToDecimal(parametersFilter.MarketRisk)).Where(x => x.Ticker.Equals(parametersFilter.Ticker.ToUpper()))
                                                                                                                              .FirstOrDefault();

            if (tickerGreenBlatt == null && tickerPriceAndProfit == null && tickerEvEbit == null && tickerBazin == null && tickerGraham == null && tickerGordon == null)
                throw new FormulaException("Não foi encontrado resultado valídos para o Ticker: " + parametersFilter.Ticker);

            return ItemTickersAnalisys(tickerGreenBlatt, tickerEvEbit, tickerPriceAndProfit, tickerBazin, tickerGordon, tickerGraham);
        }

        private FormulaDtoPosition ItemTickersAnalisys(FormulaDtoGreenBlatt? tickerGreenBlatt, FormulaDtoEvEbit? tickerEvEbit,
            FormulaDtoPriceAndProfit? tickerPriceAndProfit, FormulaDtoBazin? tickerBazin, FormulaDtoGordon? tickerGordon, FormulaDtoGraham? tickerGraham)
        {
            FormulaDtoPosition tickerPosition = new FormulaDtoPosition();

            tickerPosition.NameSeguiment = (tickerGreenBlatt != null ? tickerGreenBlatt.NameSeguiment :
                                            tickerEvEbit != null ? tickerEvEbit.NameSeguiment :
                                            tickerPriceAndProfit != null ? tickerPriceAndProfit.NameSeguiment :
                                            tickerBazin != null ? tickerBazin.NameSeguiment :
                                            tickerGordon != null ? tickerGordon.NameSeguiment :
                                            tickerGraham != null ? tickerGraham.NameSeguiment : null);

            tickerPosition.Ticker = (tickerGreenBlatt != null ? tickerGreenBlatt.Ticker :
                                     tickerEvEbit != null ? tickerEvEbit.Ticker :
                                     tickerPriceAndProfit != null ? tickerPriceAndProfit.Ticker :
                                     tickerBazin != null ? tickerBazin.Ticker :
                                     tickerGordon != null ? tickerGordon.Ticker :
                                     tickerGraham != null ? tickerGraham.Ticker : null);

            tickerPosition.Price = (tickerGreenBlatt != null ? tickerGreenBlatt.Price :
                                    tickerEvEbit != null ? tickerEvEbit.Price :
                                    tickerPriceAndProfit != null ? tickerPriceAndProfit.Price :
                                    tickerBazin != null ? tickerBazin.Price :
                                    tickerGordon != null ? tickerGordon.Price :
                                    tickerGraham != null ? tickerGraham.Price : 0);

            tickerPosition.PriceByProfit = (tickerGreenBlatt != null ? tickerGreenBlatt.PriceByProfit :
                                            tickerEvEbit != null ? tickerEvEbit.PriceByProfit :
                                            tickerPriceAndProfit != null ? tickerPriceAndProfit.PriceByProfit :
                                            tickerBazin != null ? tickerBazin.PriceByProfit :
                                            tickerGordon != null ? tickerGordon.PriceByProfit :
                                            tickerGraham != null ? tickerGraham.PriceByProfit : 0);

            tickerPosition.DividendYield = (tickerGreenBlatt != null ? tickerGreenBlatt.DividendYield :
                                            tickerEvEbit != null ? tickerEvEbit.DividendYield :
                                            tickerPriceAndProfit != null ? tickerPriceAndProfit.DividendYield :
                                            tickerBazin != null ? tickerBazin.DividendYield :
                                            tickerGordon != null ? tickerGordon.DividendYield :
                                            tickerGraham != null ? tickerGraham.DividendYield : 0);

            tickerPosition.EvEbit = (tickerGreenBlatt != null ? tickerGreenBlatt.EvEbit :
                                     tickerEvEbit != null ? tickerEvEbit.EvEbit :
                                     tickerPriceAndProfit != null ? tickerPriceAndProfit.EvEbit :
                                     tickerBazin != null ? tickerBazin.EvEbit :
                                     tickerGordon != null ? tickerGordon.EvEbit :
                                     tickerGraham != null ? tickerGraham.EvEbit : 0);

            tickerPosition.Roic = (tickerGreenBlatt != null ? tickerGreenBlatt.Roic :
                                   tickerEvEbit != null ? tickerEvEbit.Roic :
                                   tickerPriceAndProfit != null ? tickerPriceAndProfit.Roic :
                                   tickerBazin != null ? tickerBazin.Roic :
                                   tickerGordon != null ? tickerGordon.Roic :
                                   tickerGraham != null ? tickerGraham.Roic : 0);

            tickerPosition.Roe = (tickerGreenBlatt != null ? tickerGreenBlatt.Roe :
                                  tickerEvEbit != null ? tickerEvEbit.Roe :
                                  tickerPriceAndProfit != null ? tickerPriceAndProfit.Roe :
                                  tickerBazin != null ? tickerBazin.Roe :
                                  tickerGordon != null ? tickerGordon.Roe :
                                  tickerGraham != null ? tickerGraham.Roe : 0);

            tickerPosition.Vpa = (tickerGreenBlatt != null ? tickerGreenBlatt.Vpa :
                                  tickerEvEbit != null ? tickerEvEbit.Vpa :
                                  tickerPriceAndProfit != null ? tickerPriceAndProfit.Vpa :
                                  tickerBazin != null ? tickerBazin.Vpa :
                                  tickerGordon != null ? tickerGordon.Vpa :
                                  tickerGraham != null ? tickerGraham.Vpa : 0);

            tickerPosition.Lpa = (tickerGreenBlatt != null ? tickerGreenBlatt.Lpa :
                                  tickerEvEbit != null ? tickerEvEbit.Lpa :
                                  tickerPriceAndProfit != null ? tickerPriceAndProfit.Lpa :
                                  tickerBazin != null ? tickerBazin.Lpa :
                                  tickerGordon != null ? tickerGordon.Lpa :
                                  tickerGraham != null ? tickerGraham.Lpa : 0);

            tickerPosition.Dpa = (tickerGreenBlatt != null ? tickerGreenBlatt.Dpa :
                                  tickerEvEbit != null ? tickerEvEbit.Dpa :
                                  tickerPriceAndProfit != null ? tickerPriceAndProfit.Dpa :
                                  tickerBazin != null ? tickerBazin.Dpa :
                                  tickerGordon != null ? tickerGordon.Dpa :
                                  tickerGraham != null ? tickerGraham.Dpa : null);

            tickerPosition.JudicialRecovery = (tickerGreenBlatt != null ? tickerGreenBlatt.JudicialRecovery :
                                               tickerEvEbit != null ? tickerEvEbit.JudicialRecovery :
                                               tickerPriceAndProfit != null ? tickerPriceAndProfit.JudicialRecovery :
                                               tickerBazin != null ? tickerBazin.JudicialRecovery :
                                               tickerGordon != null ? tickerGordon.JudicialRecovery :
                                               tickerGraham != null ? tickerGraham.JudicialRecovery : false);

            tickerPosition.Payout = (tickerGreenBlatt != null ? tickerGreenBlatt.Payout :
                                     tickerEvEbit != null ? tickerEvEbit.Payout :
                                     tickerPriceAndProfit != null ? tickerPriceAndProfit.Payout :
                                     tickerBazin != null ? tickerBazin.Payout :
                                     tickerGordon != null ? tickerGordon.Payout :
                                     tickerGraham != null ? tickerGraham.Payout : null);

            tickerPosition.EbitMargin = (tickerGreenBlatt != null ? tickerGreenBlatt.EbitMargin :
                                         tickerEvEbit != null ? tickerEvEbit.EbitMargin :
                                         tickerPriceAndProfit != null ? tickerPriceAndProfit.EbitMargin :
                                         tickerBazin != null ? tickerBazin.EbitMargin :
                                         tickerGordon != null ? tickerGordon.EbitMargin :
                                         tickerGraham != null ? tickerGraham.EbitMargin : 0);

            tickerPosition.ProfitCAGR = (tickerGreenBlatt != null ? tickerGreenBlatt.ProfitCAGR :
                                         tickerEvEbit != null ? tickerEvEbit.ProfitCAGR :
                                         tickerPriceAndProfit != null ? tickerPriceAndProfit.ProfitCAGR :
                                         tickerBazin != null ? tickerBazin.ProfitCAGR :
                                         tickerGordon != null ? tickerGordon.ProfitCAGR :
                                         tickerGraham != null ? tickerGraham.ProfitCAGR : null);

            tickerPosition.AverageGrowth = (tickerGreenBlatt != null ? tickerGreenBlatt.AverageGrowth :
                                            tickerEvEbit != null ? tickerEvEbit.AverageGrowth :
                                            tickerPriceAndProfit != null ? tickerPriceAndProfit.AverageGrowth :
                                            tickerBazin != null ? tickerBazin.AverageGrowth :
                                            tickerGordon != null ? tickerGordon.AverageGrowth :
                                            tickerGraham != null ? tickerGraham.AverageGrowth : 0);

            tickerPosition.ExpectedGrowth = (tickerGreenBlatt != null ? tickerGreenBlatt.ExpectedGrowth :
                                             tickerEvEbit != null ? tickerEvEbit.ExpectedGrowth :
                                             tickerPriceAndProfit != null ? tickerPriceAndProfit.ExpectedGrowth :
                                             tickerBazin != null ? tickerBazin.ExpectedGrowth :
                                             tickerGordon != null ? tickerGordon.ExpectedGrowth :
                                             tickerGraham != null ? tickerGraham.ExpectedGrowth : 0);

            tickerPosition.AverageDailyLiquidity = (tickerGreenBlatt != null ? tickerGreenBlatt.AverageDailyLiquidity :
                                                    tickerEvEbit != null ? tickerEvEbit.AverageDailyLiquidity :
                                                    tickerPriceAndProfit != null ? tickerPriceAndProfit.AverageDailyLiquidity :
                                                    tickerBazin != null ? tickerBazin.AverageDailyLiquidity :
                                                    tickerGordon != null ? tickerGordon.AverageDailyLiquidity :
                                                    tickerGraham != null ? tickerGraham.AverageDailyLiquidity : 0);

            tickerPosition.DiscountPercentageBazin = (tickerBazin != null ? tickerBazin.DiscountPercentage : null);
            tickerPosition.JustPriceBazin = (tickerBazin != null ? tickerBazin.JustPrice : null);

            tickerPosition.JustPriceGordon = (tickerGordon != null ? tickerGordon.JustPrice : null);
            tickerPosition.DiscountPercentageGordon = (tickerGordon != null ? tickerGordon.DiscountPercentage : null);
            tickerPosition.MarketRisk = (tickerGordon != null ? tickerGordon.MarketRisk : 0);

            tickerPosition.JustPriceGraham = (tickerGraham != null ? tickerGraham.JustPrice : null);
            tickerPosition.DiscountPercentageGraham = (tickerGraham != null ? tickerGraham.DiscountPercentage : null);

            tickerPosition.PositionEvEbit = (tickerEvEbit != null ? tickerEvEbit.Position : null);
            tickerPosition.PositionGreenBlatt = (tickerGreenBlatt != null ? tickerGreenBlatt.Position : null);
            tickerPosition.PositionPriceAndProfit = (tickerPriceAndProfit != null ? tickerPriceAndProfit.Position : null);

            return tickerPosition;
        }

        public async Task<IEnumerable<FormulaDtoPosition>> ListTickersAnalisys(ParametersFilter parametersFilter)
        {
            if (parametersFilter.MarketRisk == null || parametersFilter.MarketRisk <= 0)
                throw new FormulaException("Deve ser informado um valor válido, acima de ZERO, para o parametro Risco de Mercado");

            var tickersGreenBlatt = ReturnListOrderedGreenBlatt(await ReturnListWithParametersExecuted(parametersFilter));

            var tickersEvEbit = ReturnListOrderedEvEbit(await ReturnListWithParametersExecuted(parametersFilter));

            var tickersPriceAndProfit = ReturnListOrderedPriceAndProfit(await ReturnListWithParametersExecuted(parametersFilter));

            var tickersBazin = ReturnListValuetionByBazin(await ReturnListWithParametersExecuted(parametersFilter));

            var tickersGordon = ReturnListValuetionByGordon(await ReturnListWithParametersExecuted(parametersFilter), Convert.ToDecimal(parametersFilter.MarketRisk));

            var tickersGraham = ReturnListValuetionByGraham(await ReturnListWithParametersExecuted(parametersFilter));

            List<FormulaDtoPosition> listTickerPosition = new List<FormulaDtoPosition>();

            // Insere inicialmente todos os itens baseados na formula de greenblatt
            foreach (var item in tickersGreenBlatt)
            {
                listTickerPosition.Add(ItemTickersAnalisys(item, null, null, null, null, null));
            }

            // valida se já existe na lista e insere os atualiza os dados baseado na formula de Ev/Ebit
            foreach (var item in tickersEvEbit)
            {
                var tickerPosition = listTickerPosition.Where(x => x.Ticker.Equals(item.Ticker)).FirstOrDefault();

                if (tickerPosition != null)
                {
                    // excluí o item que existia
                    listTickerPosition.Remove(tickerPosition);
                    // atualiza as informações
                    tickerPosition.PositionEvEbit = item.Position;
                    // insere novamente o teim atualizado
                    listTickerPosition.Add(tickerPosition);
                }
                else
                {
                    listTickerPosition.Add(ItemTickersAnalisys(null, item, null, null, null, null));
                }
            }

            // valida se já existe na lista e insere os atualiza os dados baseado na formula de P/L
            foreach (var item in tickersPriceAndProfit)
            {
                var tickerPosition = listTickerPosition.Where(x => x.Ticker.Equals(item.Ticker)).FirstOrDefault();

                if (tickerPosition != null)
                {
                    // excluí o item que existia
                    listTickerPosition.Remove(tickerPosition);
                    // atualiza as informações
                    tickerPosition.PositionPriceAndProfit = item.Position;
                    // insere novamente o teim atualizado
                    listTickerPosition.Add(tickerPosition);
                }
                else
                {
                    listTickerPosition.Add(ItemTickersAnalisys(null, null, item, null, null, null));
                }
            }

            // valida se já existe na lista e insere os atualiza os dados baseado na formula de Bazin
            foreach (var item in tickersBazin)
            {
                var tickerPosition = listTickerPosition.Where(x => x.Ticker.Equals(item.Ticker)).FirstOrDefault();

                if (tickerPosition != null)
                {
                    // excluí o item que existia
                    listTickerPosition.Remove(tickerPosition);
                    // atualiza as informações
                    tickerPosition.DiscountPercentageBazin = item.DiscountPercentage;
                    tickerPosition.JustPriceBazin = item.JustPrice;
                    // insere novamente o teim atualizado
                    listTickerPosition.Add(tickerPosition);
                }
                else
                {
                    listTickerPosition.Add(ItemTickersAnalisys(null, null, null, item, null, null));
                }
            }

            // valida se já existe na lista e insere os atualiza os dados baseado na formula de Gordon
            foreach (var item in tickersGordon)
            {
                var tickerPosition = listTickerPosition.Where(x => x.Ticker.Equals(item.Ticker)).FirstOrDefault();

                if (tickerPosition != null)
                {
                    // excluí o item que existia
                    listTickerPosition.Remove(tickerPosition);
                    // atualiza as informações
                    tickerPosition.DiscountPercentageGordon = item.DiscountPercentage;
                    tickerPosition.JustPriceGordon = item.JustPrice;
                    // insere novamente o teim atualizado
                    listTickerPosition.Add(tickerPosition);
                }
                else
                {
                    listTickerPosition.Add(ItemTickersAnalisys(null, null, null, null, item, null));
                }
            }

            // valida se já existe na lista e insere os atualiza os dados baseado na formula de Graham
            foreach (var item in tickersGraham)
            {
                var tickerPosition = listTickerPosition.Where(x => x.Ticker.Equals(item.Ticker)).FirstOrDefault();

                if (tickerPosition != null)
                {
                    // excluí o item que existia
                    listTickerPosition.Remove(tickerPosition);
                    // atualiza as informações
                    tickerPosition.DiscountPercentageGraham = item.DiscountPercentage;
                    tickerPosition.JustPriceGraham = item.JustPrice;
                    // insere novamente o teim atualizado
                    listTickerPosition.Add(tickerPosition);
                }
                else
                {
                    listTickerPosition.Add(ItemTickersAnalisys(null, null, null, null, null, item));
                }
            }

            return listTickerPosition.OrderBy(x => x.Ticker);
        }
    }
}
