using System.Collections.Generic;
using System.Threading.Tasks;
using App.Domain.Dtos.HistoryTicker;

namespace App.Domain.Interfaces.Services.HistoryTicker
{
    public interface IHistoryTickerService
    {
        Task<IEnumerable<HistoryTickerDto>> GetAllByFileImport(int fileImportId);
        Task<IEnumerable<HistoryTickerDtoComplete>> GetAllByFileImportComplete(int fileImportId);
        Task<IEnumerable<HistoryTickerDtoComplete>> GetAllByTicker(string ticker);
        Task<HistoryTickerDtoCreateResult> InsertHistoryTicker(HistoryTickerDtoCreate historyTicker);
        Task<bool> DeleteHistoryTickertById(int id);
        Task<bool> DeleteHistoryTickerByFileImport(int fileImportId);
    }
}