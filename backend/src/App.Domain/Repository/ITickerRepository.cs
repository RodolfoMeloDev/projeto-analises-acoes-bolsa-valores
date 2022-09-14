using System.Collections.Generic;
using System.Threading.Tasks;
using App.Domain.Entities;
using App.Domain.Interfaces.Services;

namespace App.Domain.Repository
{
    public interface ITickerRepository : IRepository<TickerEntity>
    {
        Task<TickerEntity> GetByIdComplete(int id);
        Task<TickerEntity> GetByTickerComplete(string ticker);
        Task<IEnumerable<TickerEntity>> GetAllComplete();
        Task<IEnumerable<TickerEntity>> GetBySegmentoId(int segmentId);
        Task<IEnumerable<TickerEntity>> GetBySubSectorId(int subSectorId);
        Task<IEnumerable<TickerEntity>> GetBySectorId(int sectorId);
        Task<TickerEntity> ExistTicker(string ticker);
    }
}