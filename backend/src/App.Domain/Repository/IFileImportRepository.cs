using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using App.Domain.Entities;
using App.Domain.Interfaces.Services;

namespace App.Domain.Repository
{
    public interface IFileImportRepository : IRepository<FileImportEntity>
    {
        Task<FileImportEntity> GetByDate(int userId, DateTime date);
        Task<IEnumerable<FileImportEntity>> GetAllFileImport(int userId);
    }
}