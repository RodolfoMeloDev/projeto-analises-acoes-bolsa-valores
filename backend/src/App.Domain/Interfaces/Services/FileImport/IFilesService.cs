using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace App.Domain.Interfaces.Services.FileImport
{
    public interface IFilesService<T>
    {
        IEnumerable<T> GetLinesFile(IFormFile file, string directoryUser);
        Task<bool> InsertListTickers(IEnumerable<T> lines);
        Task<bool> InsertHistoryTickers(IEnumerable<T> lines, int fileImportId);
        Task<bool> DeleteHistoryTickers(int fileImportId);
    }
}
