using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using App.Domain.Dtos.FileImport;

namespace App.Domain.Interfaces.Services.FileImport
{
    public interface IFileImportService
    {
        Task<FileImportDto> GetFileImportById(int id);
        Task<IEnumerable<FileImportDto>> GetAllFileImport(int userId);
        Task<FileImportDto> GetFileImportByDate(int userId, DateTime date);
        Task<FileImportDtoCreateResult> InsertFileImport(FileImportDtoCreate fileImport);
        Task<bool> DeleteFileImport(int id);
    }
}