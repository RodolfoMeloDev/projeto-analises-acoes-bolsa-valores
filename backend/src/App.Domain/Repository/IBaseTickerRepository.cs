using System.Collections.Generic;
using System.Threading.Tasks;
using App.Domain.Entities;
using App.Domain.Interfaces.Services;

namespace App.Domain.Repository
{
    public interface IBaseTickerRepository : IRepository<BaseTickerEntity>
    {
        Task<IEnumerable<BaseTickerEntity>> GetAllBySegment(int segment);
        Task<IEnumerable<BaseTickerEntity>> GetAllBySubSector(int subSector);
        Task<IEnumerable<BaseTickerEntity>> GetAllBySector(int sector);
    }
}