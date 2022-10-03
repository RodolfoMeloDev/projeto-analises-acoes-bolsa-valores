using System.Collections.Generic;
using System.Threading.Tasks;
using App.Domain.Dtos.BaseTicker;

namespace App.Domain.Interfaces.Services.BaseTicker
{
    public interface IBaseTickerService
    {
        Task<IEnumerable<BaseTickerDto>> GetAll();
    }
}
