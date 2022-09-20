using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using App.Domain.Dtos.FileImport;

namespace App.Domain.Interfaces.Services.FileImport
{
    public interface IFileImportService
    {
        Task<FileImportDto> GetFileImportById(int id);
        Task<IEnumerable<FileImportDto>> GetAllFileImport();
        Task<IEnumerable<FileImportDto>> GetFileImportPerPeriod(DateTime dateInitial, DateTime dateFinal);
        Task<FileImportDto> GetFileImportByDate(DateTime date);
        Task<FileImportDtoCreateResult> InsertFileImport(FileImportDtoCreate fileImport);
        Task<bool> DeleteFileImport(int id);
    }
}