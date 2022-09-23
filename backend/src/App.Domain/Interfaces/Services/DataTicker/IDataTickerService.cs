using System.Collections.Generic;
using System.Threading.Tasks;
using App.Domain.Models.DataTicker;

namespace App.Domain.Interfaces.Services.DataTicker
{
    public interface IDataTickerService
    {
        Task<IEnumerable<DataTickerModel>> GetDataAllTicker();
        Task<bool> ImportSegmentsSubSectorsAndSectors(IEnumerable<DataTickerModel> tickres);
    }
}
