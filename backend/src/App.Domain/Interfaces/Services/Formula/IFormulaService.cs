using System.Collections.Generic;
using System.Threading.Tasks;
using App.Domain.Dtos.Formula;

namespace App.Domain.Interfaces.Services.Formula
{
    public interface IFormulaService
    {
        Task<IEnumerable<FormulaDto>> Greenblatt(int fileImportId);
        Task<IEnumerable<FormulaDto>> Greenblatt(OptionsFormula optionsFormula);
        Task<IEnumerable<FormulaDto>> PriceAndProfit(int fileImportId);
        Task<IEnumerable<FormulaDto>> PriceAndProfit(OptionsFormula optionsFormula);
    }
}
