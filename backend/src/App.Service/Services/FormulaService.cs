using App.Domain.Dtos.Formula;
using App.Domain.Dtos.HistoryTicker;
using App.Domain.Interfaces.Services.Formula;
using App.Domain.Interfaces.Services.HistoryTicker;

namespace App.Service.Services
{
    public class FormulaService : IFormulaService
    {
        private IHistoryTickerService _historyTickerService;

        public FormulaService(IHistoryTickerService historyTickerService)
        {
            _historyTickerService = historyTickerService;
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
                ticker.DividendYield = (item.DividendYield == null ? 0 : (decimal)item.DividendYield);
                ticker.PrecoLucro = item.PrecoLucro;
                ticker.EvEbit = item.EvEbit;
                ticker.Roic = item.Roic;
                ticker.MargemEbit = item.MargemEbit;
                ticker.LiquidezMediaDiaria = (item.LiquidezMediaDiaria == null ? 0 : (decimal)item.LiquidezMediaDiaria);
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
                ticker.DividendYield = (item.DividendYield == null ? 0 : (decimal)item.DividendYield);
                ticker.PrecoLucro = item.PrecoLucro;
                ticker.EvEbit = item.EvEbit;
                ticker.Roic = item.Roic;
                ticker.MargemEbit = item.MargemEbit;
                ticker.LiquidezMediaDiaria = (item.LiquidezMediaDiaria == null ? 0 : (decimal)item.LiquidezMediaDiaria);
                ticker.RecuperacaoJudicial = item.Ticker.RecuperacaoJudicial;
                ticker.PontuacaoFinal = (listTickers.Count() - nPosicao);

                listReturn.Add(ticker);
            }

            return listReturn.OrderByDescending(obj => obj.PontuacaoFinal);
        }

        private IOrderedEnumerable<FormulaDto> ReturnListOrderedValuetionByBazin(IEnumerable<HistoryTickerDtoComplete> listTickers)
        {
            List<FormulaDto> listReturn = new List<FormulaDto>();
            int nPosicao = 0;

            foreach (var item in listTickers)
            {
                if (item.Dpa == 0 || item.Dpa == null)
                {
                    continue;
                }

                nPosicao++;
                var ticker = new FormulaDto();

                ticker.BaseTicker = item.Ticker.BaseTicker;
                ticker.Ticker = item.Ticker.Ticker;
                ticker.Preco = item.PrecoUnitario;
                ticker.DividendYield = (item.DividendYield == null ? 0 : (decimal)item.DividendYield);
                ticker.PrecoLucro = item.PrecoLucro;
                ticker.EvEbit = item.EvEbit;
                ticker.Roic = item.Roic;
                ticker.MargemEbit = item.MargemEbit;
                ticker.LiquidezMediaDiaria = (item.LiquidezMediaDiaria == null ? 0 : (decimal)item.LiquidezMediaDiaria);
                ticker.RecuperacaoJudicial = item.Ticker.RecuperacaoJudicial;

                decimal _precoJusto = (decimal)item.Dpa / (decimal)0.06;

                ticker.Desconto = decimal.Round(((_precoJusto - item.PrecoUnitario) / _precoJusto) * 100, 2);

                listReturn.Add(ticker);
            }

            return listReturn.OrderByDescending(obj => obj.Desconto);
        }

        private IOrderedEnumerable<FormulaDto> ReturnListOrderedValuetionByGraham(IEnumerable<HistoryTickerDtoComplete> listTickers)
        {
            // var _precoLucroRemove = listTickers.Where(obj => obj.PrecoLucro > 15);
            var _vpaRemove = listTickers.Where(obj => obj.Vpa <= 0);
            var _lpaRemove = listTickers.Where(obj => obj.Lpa <= 0);

            listTickers = listTickers.Except(_vpaRemove)
                                     .Except(_lpaRemove)
                                     .ToList();

            List<FormulaDto> listReturn = new List<FormulaDto>();
            int nPosicao = 0;

            foreach (var item in listTickers)
            {
                nPosicao++;
                var ticker = new FormulaDto();

                ticker.BaseTicker = item.Ticker.BaseTicker;
                ticker.Ticker = item.Ticker.Ticker;
                ticker.Preco = item.PrecoUnitario;
                ticker.DividendYield = (item.DividendYield == null ? 0 : (decimal)item.DividendYield);
                ticker.PrecoLucro = item.PrecoLucro;
                ticker.EvEbit = item.EvEbit;
                ticker.Roic = item.Roic;
                ticker.MargemEbit = item.MargemEbit;
                ticker.LiquidezMediaDiaria = (item.LiquidezMediaDiaria == null ? 0 : (decimal)item.LiquidezMediaDiaria);
                ticker.RecuperacaoJudicial = item.Ticker.RecuperacaoJudicial;

                var valor = (decimal?)22.5 * item.Lpa * item.Vpa;

                var isNegativo = (valor < 0 ? true : false);

                valor = (isNegativo ? valor * -1 : valor * 1);

                decimal _precoJusto = (decimal)Math.Sqrt((double)valor);

                _precoJusto = (isNegativo ? _precoJusto * -1 : _precoJusto * 1);

                ticker.Desconto = decimal.Round(((_precoJusto - item.PrecoUnitario) / _precoJusto) * 100, 2);

                listReturn.Add(ticker);
            }

            return listReturn.OrderByDescending(obj => obj.Desconto);
        }

        public async Task<IEnumerable<FormulaDto>> Greenblatt(OptionsFormula optionsFormula)
        {
            try
            {
                return ReturnListOrderedGreenBlatt(
                    await ReturnListWithParametersExecuted(optionsFormula));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<IEnumerable<FormulaDto>> Greenblatt(int fileImportId)
        {
            try
            {
                var historyTicker = await _historyTickerService.GetAllByFileImportComplete(fileImportId);

                return ReturnListOrderedGreenBlatt(historyTicker);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<IEnumerable<FormulaDto>> PriceAndProfit(int fileImportId)
        {
            try
            {
                var historyTicker = await _historyTickerService.GetAllByFileImportComplete(fileImportId);

                return ReturnListOrderedPriceAndProfit(historyTicker);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<IEnumerable<FormulaDto>> PriceAndProfit(OptionsFormula optionsFormula)
        {
            try
            {
                return ReturnListOrderedPriceAndProfit(
                    await ReturnListWithParametersExecuted(optionsFormula));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<IEnumerable<FormulaDto>> ValuetionByBazin(int fileImportId)
        {
            try
            {
                var historyTicker = await _historyTickerService.GetAllByFileImportComplete(fileImportId);

                return ReturnListOrderedValuetionByBazin(historyTicker);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<IEnumerable<FormulaDto>> ValuetionByBazin(OptionsFormula optionsFormula)
        {
            try
            {
                return ReturnListOrderedValuetionByBazin(
                    await ReturnListWithParametersExecuted(optionsFormula));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<IEnumerable<FormulaDto>> ValuetionByGraham(int fileImportId)
        {
            try
            {
                var historyTicker = await _historyTickerService.GetAllByFileImportComplete(fileImportId);

                return ReturnListOrderedValuetionByGraham(historyTicker);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<IEnumerable<FormulaDto>> ValuetionByGraham(OptionsFormula optionsFormula)
        {
            try
            {
                return ReturnListOrderedValuetionByGraham(
                    await ReturnListWithParametersExecuted(optionsFormula));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
