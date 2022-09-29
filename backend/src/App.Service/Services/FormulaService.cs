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

        private async Task<IEnumerable<HistoryTickerDtoComplete>> ReturnListWithParametersExecuted(OptionsFormula optionsFormula)
        {
            var historyTicker = await _historyTickerService.GetAllByFileImportComplete(optionsFormula.FileImportId);

            var liqMedDiariaRemove = historyTicker.Where(obj => obj.LiquidezMediaDiaria == null);
            historyTicker = historyTicker.Except(liqMedDiariaRemove)
                                         .ToList();

            if (optionsFormula.RemoverItensComValorNegativo)
            {
                var evEbitNegative = historyTicker.Where(obj => obj.EvEbit < 0);
                var precoLucroNegative = historyTicker.Where(obj => obj.PrecoLucro < 0);

                historyTicker = historyTicker.Except(evEbitNegative)
                                             .Except(precoLucroNegative)
                                             .ToList();
            }

            if (optionsFormula.RemoverItensComValorZerado)
            {
                var evEbitZero = historyTicker.Where(obj => obj.EvEbit == 0);
                var precoLucroZero = historyTicker.Where(obj => obj.PrecoLucro == 0);

                historyTicker = historyTicker.Except(evEbitZero)
                                             .Except(precoLucroZero)
                                             .ToList();
            }

            if (optionsFormula.LiquidezMinima != null)
            {
                var itensRemove = historyTicker.Where(obj => obj.LiquidezMediaDiaria < optionsFormula.LiquidezMinima);

                historyTicker = historyTicker.Except(itensRemove)
                                             .ToList();
            }

            if (optionsFormula.EvEbitMinima != null)
            {
                var itensRemove = historyTicker.Where(obj => obj.EvEbit < optionsFormula.EvEbitMinima);

                historyTicker = historyTicker.Except(itensRemove)
                                             .ToList();
            }

            if (optionsFormula.EvEbitMaxima != null)
            {
                var itensRemove = historyTicker.Where(obj => obj.EvEbit > optionsFormula.EvEbitMaxima);

                historyTicker = historyTicker.Except(itensRemove)
                                             .ToList();
            }

            if (optionsFormula.PLMinima != null)
            {
                var itensRemove = historyTicker.Where(obj => obj.PrecoLucro < optionsFormula.PLMinima);

                historyTicker = historyTicker.Except(itensRemove)
                                             .ToList();
            }

            if (optionsFormula.PLMaxima != null)
            {
                var itensRemove = historyTicker.Where(obj => obj.PrecoLucro > optionsFormula.PLMaxima);

                historyTicker = historyTicker.Except(itensRemove)
                                             .ToList();
            }

            if (optionsFormula.MargemEbitMinima != null)
            {
                var itensRemove = historyTicker.Where(obj => obj.MargemEbit < optionsFormula.MargemEbitMinima);

                historyTicker = historyTicker.Except(itensRemove)
                                             .ToList();
            }

            if (optionsFormula.MargemEbitMaxima != null)
            {
                var itensRemove = historyTicker.Where(obj => obj.MargemEbit > optionsFormula.MargemEbitMaxima);

                historyTicker = historyTicker.Except(itensRemove)
                                             .ToList();
            }

            if (!optionsFormula.IncluirItensRecuperacaoJudicial)
            {
                var itensRemove = historyTicker.Where(obj => obj.Ticker.RecuperacaoJudicial.Equals(true));

                historyTicker = historyTicker.Except(itensRemove)
                                             .ToList();
            }

            var historyTickerOrdered = historyTicker.OrderBy(obj => obj.Ticker.BaseTicker).ThenByDescending(obj => obj.LiquidezMediaDiaria);

            if (optionsFormula.RemoverMenorLiquidez)
            {
                string lastBaseTicker = string.Empty;
                IList<HistoryTickerDtoComplete> itensRemove = new List<HistoryTickerDtoComplete>();

                foreach (var item in historyTickerOrdered)
                {
                    if (item.Ticker.BaseTicker.Equals(lastBaseTicker))
                    {
                        itensRemove.Add(item);
                    }

                    lastBaseTicker = item.Ticker.BaseTicker;
                }

                historyTicker = historyTicker.Except(itensRemove)
                                             .ToList();
            }

            return historyTicker;
        }

        private IOrderedEnumerable<FormulaDto> ReturnListOrderedGreenBlatt(IEnumerable<HistoryTickerDtoComplete> listTickers)
        {
            var historyTickerOrdered = listTickers.OrderBy(obj => obj.EvEbit);

            List<FormulaDto> listReturn = new List<FormulaDto>();
            int nPosicao = 0;

            foreach (var item in historyTickerOrdered)
            {
                nPosicao++;
                var ticker = new FormulaDto();

                ticker.BaseTicker = item.Ticker.BaseTicker;
                ticker.Ticker = item.Ticker.Ticker;
                ticker.Preco = item.PrecoUnitario;
                ticker.DividendYield = Convert.ToDecimal(item.DividendYield);
                ticker.PrecoLucro = item.PrecoLucro;
                ticker.EvEbit = item.EvEbit;
                ticker.Roic = item.Roic;
                ticker.MargemEbit = item.MargemEbit;
                ticker.LiquidezMediaDiaria = Convert.ToDecimal(item.LiquidezMediaDiaria);
                ticker.RecuperacaoJudicial = item.Ticker.RecuperacaoJudicial;
                ticker.PontuacaoEvEbit = (listTickers.Count() - nPosicao);

                listReturn.Add(ticker);
            }

            historyTickerOrdered = listTickers.OrderByDescending(obj => obj.Roic);
            nPosicao = 0;

            foreach (var item in historyTickerOrdered)
            {
                nPosicao++;
                var index = listReturn.FindIndex(0, listReturn.Count(), o => o.Ticker.Equals(item.Ticker.Ticker));
                listReturn[index].PontuacaoRoic = (listTickers.Count() - nPosicao);
                listReturn[index].PontuacaoFinal = listReturn[index].PontuacaoRoic + listReturn[index].PontuacaoEvEbit;
            }

            return listReturn.OrderByDescending(obj => obj.PontuacaoFinal)
                             .ThenByDescending(obj => obj.LiquidezMediaDiaria);
        }

        private IOrderedEnumerable<FormulaDto> ReturnListOrderedPriceAndProfit(IEnumerable<HistoryTickerDtoComplete> listTickers)
        {
            var historyTickerOrdered = listTickers.OrderBy(obj => obj.PrecoLucro);

            List<FormulaDto> listReturn = new List<FormulaDto>();
            int nPosicao = 0;

            foreach (var item in historyTickerOrdered)
            {
                nPosicao++;
                var ticker = new FormulaDto();

                ticker.BaseTicker = item.Ticker.BaseTicker;
                ticker.Ticker = item.Ticker.Ticker;
                ticker.Preco = item.PrecoUnitario;
                ticker.DividendYield = Convert.ToDecimal(item.DividendYield);
                ticker.PrecoLucro = item.PrecoLucro;
                ticker.EvEbit = item.EvEbit;
                ticker.Roic = item.Roic;
                ticker.MargemEbit = item.MargemEbit;
                ticker.LiquidezMediaDiaria = Convert.ToDecimal(item.LiquidezMediaDiaria);
                ticker.RecuperacaoJudicial = item.Ticker.RecuperacaoJudicial;
                ticker.PontuacaoFinal = (listTickers.Count() - nPosicao);

                listReturn.Add(ticker);
            }

            return listReturn.OrderByDescending(obj => obj.PontuacaoFinal);
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
                ticker.Preco = item.PrecoUnitario;
                ticker.DividendYield = Convert.ToDecimal(item.DividendYield);
                ticker.PrecoLucro = item.PrecoLucro;
                ticker.EvEbit = item.EvEbit;
                ticker.Roic = item.Roic;
                ticker.MargemEbit = item.MargemEbit;
                ticker.LiquidezMediaDiaria = Convert.ToDecimal(item.LiquidezMediaDiaria);
                ticker.RecuperacaoJudicial = item.Ticker.RecuperacaoJudicial;

                decimal _justPrice = Convert.ToDecimal(item.Dpa) / Convert.ToDecimal(0.06);

                ticker.Desconto = ReturnDiscountPercentage(item.PrecoUnitario, _justPrice);

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
                ticker.Preco = item.PrecoUnitario;
                ticker.DividendYield = Convert.ToDecimal(item.DividendYield);
                ticker.PrecoLucro = item.PrecoLucro;
                ticker.EvEbit = item.EvEbit;
                ticker.Roic = item.Roic;
                ticker.MargemEbit = item.MargemEbit;
                ticker.LiquidezMediaDiaria = Convert.ToDecimal(item.LiquidezMediaDiaria);
                ticker.RecuperacaoJudicial = item.Ticker.RecuperacaoJudicial;

                var valor = Convert.ToDecimal(22.5) * item.Lpa * item.Vpa;
                var isNegativo = (valor < 0 ? true : false);

                valor = (isNegativo ? valor * -1 : valor * 1);

                decimal _justPrice = Convert.ToDecimal(Math.Sqrt(Convert.ToDouble(valor)));
                _justPrice = (isNegativo ? _justPrice * -1 : _justPrice * 1);

                ticker.Desconto = ReturnDiscountPercentage(item.PrecoUnitario, _justPrice);

                listReturn.Add(ticker);
            }

            return listReturn.OrderBy(obj => obj.Ticker);
        }

        private IOrderedEnumerable<FormulaDto> ReturnListValuetionByGordon(IEnumerable<HistoryTickerDtoComplete> listTickers, decimal marketRisk)
        {
            var _dyRemove = listTickers.Where(obj => obj.DividendYield <= 0 || obj.DividendYield == null);
            var _dpaRemove = listTickers.Where(obj => obj.Dpa == null);
            var _cagrProfitRemove = listTickers.Where(obj => obj.CAGRLucro < 0);

            listTickers = listTickers.Except(_dyRemove)
                                     .Except(_dpaRemove)
                                     .Except(_cagrProfitRemove)
                                     .ToList();

            List<FormulaDto> listReturn = new List<FormulaDto>();

            foreach (var item in listTickers)
            {
                var ticker = new FormulaDto();

                // update to value for Zero, case necessary
                item.CAGRLucro = Convert.ToDecimal(item.CAGRLucro);

                ticker.BaseTicker = item.Ticker.BaseTicker;
                ticker.Ticker = item.Ticker.Ticker;
                ticker.Preco = item.PrecoUnitario;
                ticker.DividendYield = Convert.ToDecimal(item.DividendYield);
                ticker.PrecoLucro = item.PrecoLucro;
                ticker.EvEbit = item.EvEbit;
                ticker.Roic = item.Roic;
                ticker.MargemEbit = item.MargemEbit;
                ticker.LiquidezMediaDiaria = Convert.ToDecimal(item.LiquidezMediaDiaria);
                ticker.RecuperacaoJudicial = item.Ticker.RecuperacaoJudicial;

                // 1 - reference a 1 year in future
                decimal _valueFutureDy = Convert.ToDecimal(item.CAGRLucro != 0 ? item.Dpa * (1 + (item.CAGRLucro / 100)) : item.Dpa);
                decimal _justPrice = Convert.ToDecimal(_valueFutureDy / ((marketRisk - item.CAGRLucro) / 100));

                ticker.Desconto = ReturnDiscountPercentage(item.PrecoUnitario, _justPrice);

                listReturn.Add(ticker);
            }

            return listReturn.OrderBy(obj => obj.Ticker);
        }

        public async Task<IEnumerable<FormulaDto>> Greenblatt(OptionsFormula optionsFormula)
        {
            return ReturnListOrderedGreenBlatt(await ReturnListWithParametersExecuted(optionsFormula));
        }

        public async Task<IEnumerable<FormulaDto>> PriceAndProfit(OptionsFormula optionsFormula)
        {
            return ReturnListOrderedPriceAndProfit(await ReturnListWithParametersExecuted(optionsFormula));
        }

        public async Task<IEnumerable<FormulaDto>> ValuetionByBazin(OptionsFormula optionsFormula)
        {
            return ReturnListValuetionByBazin(await ReturnListWithParametersExecuted(optionsFormula));
        }

        public async Task<IEnumerable<FormulaDto>> ValuetionByGraham(OptionsFormula optionsFormula)
        {
            return ReturnListValuetionByGraham(await ReturnListWithParametersExecuted(optionsFormula));
        }

        public async Task<IEnumerable<FormulaDto>> ValuetionByGordon(OptionsFormula optionsFormula)
        {
            if (optionsFormula.RiscoBolsa == null || optionsFormula.RiscoBolsa <= 0)
                throw new FormulaException("Deve ser informado um valor válido, acima de ZERO, para o parametro Risco de Mercado");

            return ReturnListValuetionByGordon(await ReturnListWithParametersExecuted(optionsFormula), Convert.ToDecimal(optionsFormula.RiscoBolsa));
        }
    }
}
