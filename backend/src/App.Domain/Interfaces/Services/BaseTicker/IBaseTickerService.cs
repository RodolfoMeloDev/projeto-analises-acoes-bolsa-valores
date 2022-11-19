using System.Collections.Generic;
using System.Threading.Tasks;
using App.Domain.Dtos.BaseTicker;

namespace App.Domain.Interfaces.Services.BaseTicker
{
    public interface IBaseTickerService
    {
        Task<IEnumerable<BaseTickerDto>> GetAll();
        Task<IEnumerable<BaseTickerDto>> GetAllBySegment(int segment);
        Task<IEnumerable<BaseTickerDto>> GetAllBySubSector(int subSector);
        Task<IEnumerable<BaseTickerDto>> GetAllBySector(int sector);
    }
}
