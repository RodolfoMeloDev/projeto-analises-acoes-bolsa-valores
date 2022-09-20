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

        public FileImportImplementation(AnaliseDeAcoesContext context) : base(context)
        {
            _dataSet = context.Set<FileImportEntity>();
        }

        public async Task<FileImportEntity> GetByDate(DateTime date)
        {
            return await _dataSet.FirstOrDefaultAsync(obj => obj.DataArquivo.Equals(date));
        }

        public async Task<IEnumerable<FileImportEntity>> GetPerPeriod(DateTime dateInitial, DateTime dateFinal)
        {
            return await _dataSet.Where(obj => obj.DataArquivo >= dateInitial && obj.DataArquivo <= dateFinal)
                                 .ToListAsync();
        }
    }
}