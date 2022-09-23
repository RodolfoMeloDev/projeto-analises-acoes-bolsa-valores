using System.Collections.Generic;
using System.Threading.Tasks;
using App.Domain.Dtos.Ticker;

namespace App.Domain.Interfaces.Services.Ticker
{
    public interface ITickerService
    {
        Task<IEnumerable<TickerDto>> GetAll();
        Task<IEnumerable<TickerDtoComplete>> GetAllComplete();
        Task<TickerDto> GetById(int id);
        Task<TickerDtoComplete> GetByTicker(string ticker);
        Task<TickerDtoComplete> GetByIdComplete(int id);
        Task<IEnumerable<TickerDto>> GetBySectorId(int sectorId);
        Task<IEnumerable<TickerDto>> GetBySubSectorId(int subSectorId);
        Task<IEnumerable<TickerDto>> GetBySegmentId(int segmentId);
        Task<TickerDtoCreateResult> Insert(TickerDtoCreate ticker);
        Task<TickerDtoUpdateResult> Update(TickerDtoUpdate ticker);
        Task<bool> Delete(int id);
    }
}
