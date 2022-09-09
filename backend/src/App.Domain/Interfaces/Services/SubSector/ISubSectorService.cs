using System.Collections.Generic;
using System.Threading.Tasks;
using App.Domain.Dtos.SubSector;

namespace App.Domain.Interfaces.Services.SubSector
{
    public interface ISubSectorService
    {
        Task<IEnumerable<SubSectorDto>> GetAll();
        Task<IEnumerable<SubSectorDtoComplete>> GetAllComplete();
        Task<SubSectorDto> GetById(int id);
        Task<SubSectorDtoComplete> GetByIdComplete(int id);
        Task<IEnumerable<SubSectorDto>> GetBySectorId(int sectorId);
        Task<SubSectorDtoCreateResult> Insert(SubSectorDtoCreate subSector);
        Task<SubSectorDtoUpdateResult> Update(SubSectorDtoUpdate subSector);
        Task<bool> Delete(int id);    
    }
}