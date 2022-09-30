using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Data.Context;
using App.Data.Repository;
using App.Domain.Entities;
using App.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace App.Data.Implementations
{
    public class FileImportImplementation : BaseRepository<FileImportEntity>, IFileImportRepository
    {
        private DbSet<FileImportEntity> _dataSet;

        public FileImportImplementation(StockAnalysisContext context) : base(context)
        {
            _dataSet = context.Set<FileImportEntity>();
        }

        public async Task<IEnumerable<FileImportEntity>> GetAllFileImport(int userId)
        {
            return await _dataSet.Where(obj => obj.UserId.Equals(userId))
                                 .ToListAsync();
        }

        public async Task<FileImportEntity> GetByDate(int userId, DateTime date)
        {
            return await _dataSet.FirstOrDefaultAsync(obj => obj.UserId.Equals(userId) && 
                                                             obj.DateFile.Equals(date));
        }
    }
}