using System.Collections.Generic;
using System.Threading.Tasks;
using App.Domain.Dtos.Formula;

namespace App.Domain.Interfaces.Services.Formula
{
    public interface IFormulaService
    {
        Task<IEnumerable<FormulaDtoGreenBlatt>> Greenblatt(ParametersFilter parametersFilter);
        Task<IEnumerable<FormulaDto>> PriceAndProfit(ParametersFilter parametersFilter);
        Task<IEnumerable<FormulaDtoBazin>> ValuetionByBazin(ParametersFilter parametersFilter);
        Task<IEnumerable<FormulaDtoGraham>> ValuetionByGraham(ParametersFilter parametersFilter);
        Task<IEnumerable<FormulaDtoGordon>> ValuetionByGordon(ParametersFilter parametersFilter);
    }
}
