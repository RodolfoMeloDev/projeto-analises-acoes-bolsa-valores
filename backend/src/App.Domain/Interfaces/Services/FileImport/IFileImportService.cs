using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using App.Domain.Dtos.FileImport;

namespace App.Domain.Interfaces.Services.FileImport
{
    public interface IFileImportService
    {
        Task<FileImportDto> GetById(int id);
        Task<IEnumerable<FileImportDto>> GetAll(int userId);
        Task<FileImportDto> GetByDate(int userId, DateTime date);
        Task<FileImportDtoCreateResult> Insert(FileImportDtoCreate fileImport);
        Task<bool> Delete(int id);
    }
}
