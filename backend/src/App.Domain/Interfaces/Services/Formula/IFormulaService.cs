using System.Collections.Generic;
using System.Threading.Tasks;
using App.Domain.Dtos.Formula;

namespace App.Domain.Interfaces.Services.Formula
{
    public interface IFormulaService
    {
        Task<IEnumerable<FormulaDto>> Greenblatt(OptionsFormula optionsFormula);
        Task<IEnumerable<FormulaDto>> PriceAndProfit(OptionsFormula optionsFormula);
        Task<IEnumerable<FormulaDto>> ValuetionByBazin(OptionsFormula optionsFormula);
        Task<IEnumerable<FormulaDto>> ValuetionByGraham(OptionsFormula optionsFormula);
        Task<IEnumerable<FormulaDto>> ValuetionByGordon(OptionsFormula optionsFormula);
    }
}
