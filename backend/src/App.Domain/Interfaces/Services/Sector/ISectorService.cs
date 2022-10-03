using System.Collections.Generic;
using System.Threading.Tasks;
using App.Domain.Dtos.Sector;

namespace App.Domain.Interfaces.Services.Sector
{
    public interface ISectorService
    {
        Task<SectorDto> GetById(int id);
        Task<SectorDto> GetByName(string name);
        Task<IEnumerable<SectorDto>> GetAll();
        Task<SectorDtoCreateResult> Insert(SectorDtoCreate sector);
        Task<SectorDtoUpdateResult> Update(SectorDtoUpdate sector);
        Task<bool> Delete(int id);
    }
}
