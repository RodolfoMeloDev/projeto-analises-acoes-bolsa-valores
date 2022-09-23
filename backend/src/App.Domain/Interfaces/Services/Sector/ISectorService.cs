using System.Collections.Generic;
using System.Threading.Tasks;
using App.Domain.Dtos.Sector;

namespace App.Domain.Interfaces.Services.Sector
{
    public interface ISectorService
    {
        Task<SectorDto> GetSectorById(int id);
        Task<SectorDto> GetSectorByName(string name);
        Task<IEnumerable<SectorDto>> GetAllSectors();
        Task<SectorDtoCreateResult> InsertSector(SectorDtoCreate sector);
        Task<SectorDtoUpdateResult> UpdateSector(SectorDtoUpdate sector);
        Task<bool> DeleteSector(int id);
    }
}
