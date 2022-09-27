using System.Collections.Generic;
using System.Threading.Tasks;
using App.Domain.Models.DataTicker;
using Microsoft.AspNetCore.Http;

namespace App.Domain.Interfaces.Services.FileImport
{
    public interface IFilesService<T>
    {
        IEnumerable<T> GetLinesFile(IFormFile file, string directoryUser);
        Task<bool> InsertListTickers(IEnumerable<T> lines, IEnumerable<DataTickerModel> listTickerWeb);
        Task<bool> InsertHistoryTickers(IEnumerable<T> lines, int fileImportId);
        Task<bool> DeleteHistoryTickers(int fileImportId);
    }
}
