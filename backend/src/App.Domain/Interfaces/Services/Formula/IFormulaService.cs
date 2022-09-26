using System.Collections.Generic;
using System.Threading.Tasks;
using App.Domain.Dtos.Formula;

namespace App.Domain.Interfaces.Services.Formula
{
    public interface IFormulaService
    {
        Task<IEnumerable<FormulaDto>> Greenblatt(int fileImportId);
        Task<IEnumerable<FormulaDto>> Greenblatt(OptionsFormula optionsFormula);
    }
}
