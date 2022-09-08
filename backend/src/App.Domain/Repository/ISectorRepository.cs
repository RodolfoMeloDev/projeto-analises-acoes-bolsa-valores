using System.Threading.Tasks;
using App.Domain.Entities;
using App.Domain.Interfaces.Services;

namespace App.Domain.Repository
{
    public interface ISectorRepository : IRepository<SectorEntity>
    {
        Task<SectorEntity> GetByName(string name);
    }
}