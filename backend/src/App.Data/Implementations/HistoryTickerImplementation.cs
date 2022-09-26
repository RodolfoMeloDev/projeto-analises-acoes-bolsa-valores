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
    public class HistoryTickerImplementation : BaseRepository<HistoryTickerEntity>, IHistoryTickerRepository
    {
        private DbSet<HistoryTickerEntity> _dataSet;

        public HistoryTickerImplementation(AnaliseDeAcoesContext context) : base(context)
        {
            _dataSet = context.Set<HistoryTickerEntity>();
        }

        public async Task<bool> DeleteByFileImport(int fileImportId)
        {
            try
            {
                var listResult = await _dataSet.Where(obj => obj.ArquivoImportacaoId.Equals(fileImportId))
                    .ToListAsync();

                if (listResult == null)
                    return false;

                _dataSet.RemoveRange(listResult);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<HistoryTickerEntity>> GetAllByFileImport(int fileImportId)
        {
            return await _dataSet.Where(obj => obj.ArquivoImportacaoId.Equals(fileImportId))
                                 .ToListAsync();
        }

        public async Task<IEnumerable<HistoryTickerEntity>> GetAllByFileImportComplete(int fileImportId)
        {
            return await _dataSet.Include(obj => obj.ArquivoImportacao)
                                 .Include(obj => obj.Ticker)
                                 .Where(obj => obj.ArquivoImportacaoId.Equals(fileImportId))
                                 .ToListAsync();
        }

        public async Task<IEnumerable<HistoryTickerEntity>> GetAllByTicker(string ticker)
        {
            return await _dataSet.Include(obj => obj.ArquivoImportacao)
                                 .Include(obj => obj.Ticker)
                                 .Where(obj => obj.Ticker.Ticker.ToLower().Equals(ticker.ToLower()))
                                 .ToListAsync();
        }
    }
}
