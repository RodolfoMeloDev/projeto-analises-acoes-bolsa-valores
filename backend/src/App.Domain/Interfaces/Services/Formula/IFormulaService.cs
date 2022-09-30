using System.Collections.Generic;
using System.Threading.Tasks;
using App.Domain.Dtos.Formula;

namespace App.Domain.Interfaces.Services.Formula
{
    public interface IFormulaService
    {
        Task<IEnumerable<FormulaDto>> Greenblatt(ParametersFilter parametersFilter);
        Task<IEnumerable<FormulaDto>> PriceAndProfit(ParametersFilter parametersFilter);
        Task<IEnumerable<FormulaDto>> ValuetionByBazin(ParametersFilter parametersFilter);
        Task<IEnumerable<FormulaDto>> ValuetionByGraham(ParametersFilter parametersFilter);
        Task<IEnumerable<FormulaDto>> ValuetionByGordon(ParametersFilter parametersFilter);
    }
}
