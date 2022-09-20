using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using App.Domain.Entities;
using App.Domain.Interfaces.Services;

namespace App.Domain.Repository
{
    public interface IFileImportRepository : IRepository<FileImportEntity>
    {
        Task<IEnumerable<FileImportEntity>> GetPerPeriod(DateTime dateInitial, DateTime dateFinal);
        Task<FileImportEntity> GetByDate(DateTime date);
    }
}