using System.Collections.Generic;
using System.Threading.Tasks;
using App.Domain.Dtos.Segment;

namespace App.Domain.Interfaces.Services.Segment
{
    public interface ISegmentService
    {
        Task<IEnumerable<SegmentDto>> GetAll();
        Task<IEnumerable<SegmentDtoComplete>> GetAllComplete();
        Task<SegmentDto> GetById(int id);
        Task<SegmentDtoComplete> GetByIdComplete(int id);
        Task<IEnumerable<SegmentDto>> GetBySubSectorId(int subSectorId);
        Task<IEnumerable<SegmentDto>> GetBySectorId(int sectorId);
        Task<SegmentDtoCreateResult> Insert(SegmentDtoCreate segment);
        Task<SegmentDtoUpdateResult> Update(SegmentDtoUpdate segment);
        Task<bool> Delete(int id);
    }
}
