using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

            return listReturn.OrderByDescending(obj => obj.PontuacaoFinal);
        }

        public async Task<IEnumerable<FormulaDto>> Greenblatt(OptionsFormula optionsFormula)
        {
            var historyTicker = await _historyTickerService.GetAllByFileImportComplete(optionsFormula.FileImportId);

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

            historyTickerOrdered = historyTicker.OrderBy(obj => obj.Ticker.Ticker);

            return ReturnListOrderedGreenBlatt(historyTickerOrdered);
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
    }
}
