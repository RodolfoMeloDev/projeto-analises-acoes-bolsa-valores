using App.Domain.Dtos.HistoryTicker;
using App.Domain.Interfaces.Services.HistoryTicker;

namespace App.Service.Services
{
    public class HistoryTickerService : IHistoryTickerService
    {       
        public Task<IEnumerable<HistoryTickerDto>> GetAllHistoryTicker()
        {
            throw new NotImplementedException();
        }

        public Task<HistoryTickerDtoCreate> InsertHistoryTicker(HistoryTickerDtoCreate historyFileImport)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteHistoryTickerByFileImport(int fileImportId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteHistoryTickertById(int id)
        {
            throw new NotImplementedException();
        }
    }
}