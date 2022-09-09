using System.Collections.Generic;
using System.Threading.Tasks;
using App.Domain.Entities;
using App.Domain.Interfaces.Services;

namespace App.Domain.Repository
{
    public interface ISegmentRepository : IRepository<SegmentEntity>
    {
        Task<SegmentEntity> GetByIdComplete(int id);
        Task<IEnumerable<SegmentEntity>> GetAllComplete();
        Task<IEnumerable<SegmentEntity>> GetBySubSectorId(int subSectorId);
        Task<IEnumerable<SegmentEntity>> GetBySectorId(int sectorId);
        Task<SegmentEntity> ExistSegment(string name, int subSectorId);
    }
}
