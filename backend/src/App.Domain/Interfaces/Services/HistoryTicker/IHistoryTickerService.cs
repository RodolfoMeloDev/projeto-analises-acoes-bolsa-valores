using System.Collections.Generic;
using System.Threading.Tasks;
using App.Domain.Dtos.HistoryTicker;

namespace App.Domain.Interfaces.Services.HistoryTicker
{
    public interface IHistoryTickerService
    {
        Task<IEnumerable<HistoryTickerDto>> GetAllByFileImport(int fileImportId);
        Task<IEnumerable<HistoryTickerDtoComplete>> GetAllByFileImportComplete(int fileImportId);
        Task<IEnumerable<HistoryTickerDto>> GetAllByTicker(string ticker);
        Task<HistoryTickerDtoCreateResult> Insert(HistoryTickerDtoCreate historyTicker);
        Task<bool> DeleteById(int id);
        Task<bool> DeleteByFileImport(int fileImportId);
    }
}
