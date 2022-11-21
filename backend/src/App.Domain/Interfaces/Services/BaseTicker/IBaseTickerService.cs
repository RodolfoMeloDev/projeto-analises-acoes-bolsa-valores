using System.Collections.Generic;
using System.Threading.Tasks;
using App.Domain.Dtos.BaseTicker;

namespace App.Domain.Interfaces.Services.BaseTicker
{
    public interface IBaseTickerService
    {
        Task<IEnumerable<BaseTickerDto>> GetAll();
        Task<IEnumerable<BaseTickerDtoComplete>> GetAllComplete();
        Task<IEnumerable<BaseTickerDtoComplete>> GetAllBySegment(int segment);
        Task<IEnumerable<BaseTickerDtoComplete>> GetAllBySubSector(int subSector);
        Task<IEnumerable<BaseTickerDtoComplete>> GetAllBySector(int sector);
    }
}
