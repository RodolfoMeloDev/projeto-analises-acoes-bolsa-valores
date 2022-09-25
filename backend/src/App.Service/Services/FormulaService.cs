using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Domain.Dtos.Formula;
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

        public Task<IEnumerable<FormulaDto>> Greenblatt(int fileImportId, OptionsFormula optionsFormula)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<FormulaDto>> Greenblatt(int fileImportId)
        {
            var historyTicker = await _historyTickerService.GetAllByFileImportComplete(fileImportId);

            historyTicker.OrderBy(obj => obj.Ticker.BaseTicker).OrderByDescending(obj => obj.LiquidezMediaDiaria);        

            foreach (var item in historyTicker)
            {
                var ticker = new FormulaDto();
                
                ticker.BaseTicker = item.Ticker.BaseTicker;
                ticker.Ticker = item.Ticker.Ticker;
                ticker.Preco = item.PrecoUnitario;
                ticker.DividendYield = (decimal)item.DividendYield;
                ticker.PrecoLucro = item.PrecoLucro;
                ticker.EvEbit = item.EvEbit;
                ticker.Roic = item.Roic;
                ticker.MargemEbit = item.MargemEbit;
                ticker.LiquidezMediaDiaria = (decimal)item.LiquidezMediaDiaria;
                ticker.RecuperacaoJudicial = item.Ticker.RecuperacaoJudicial;
            }

            throw new NotImplementedException();
        }
    }
}