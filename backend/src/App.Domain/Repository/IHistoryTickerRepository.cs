using System.Collections.Generic;
using System.Threading.Tasks;
using App.Domain.Entities;
using App.Domain.Interfaces.Services;

namespace App.Domain.Repository
{
    public interface IHistoryTickerRepository : IRepository<HistoryTickerEntity>
    {
        Task<bool> DeleteByFileImport(int fileImportId);
        Task<IEnumerable<HistoryTickerEntity>> GetAllByFileImport(int fileImportId);
        Task<IEnumerable<HistoryTickerEntity>> GetAllByFileImportComplete(int fileImportId);
        Task<IEnumerable<HistoryTickerEntity>> GetAllByTicker(string ticker);
    }
}
