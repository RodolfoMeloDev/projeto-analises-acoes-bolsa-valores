using System.Collections.Generic;
using System.Threading.Tasks;
using App.Domain.Entities;
using App.Domain.Interfaces.Services;

namespace App.Domain.Repository
{
    public interface ISubSectorRepository : IRepository<SubSectorEntity>
    {
        Task<SubSectorEntity> GetByIdComplete(int id);
        Task<IEnumerable<SubSectorEntity>> GetAllComplete();
        Task<IEnumerable<SubSectorEntity>> GetBySectorId(int sectorId);
        Task<SubSectorEntity> ExistSubSector(string name, int sectorId);
    }
}