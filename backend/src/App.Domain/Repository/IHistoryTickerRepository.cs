using System.Threading.Tasks;
using App.Domain.Entities;
using App.Domain.Interfaces.Services;

namespace App.Domain.Repository
{
    public interface IHistoryTickerRepository : IRepository<HistoryTickerEntity>
    {
        Task<bool> DeleteByFileImport(int fileImportId);   
    }
}