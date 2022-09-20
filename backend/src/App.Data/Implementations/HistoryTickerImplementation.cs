using System;
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
                var result = await _dataSet.FirstOrDefaultAsync(obj => obj.ArquivoImportacaoId.Equals(fileImportId));

                if (result == null)
                    return false;

                _dataSet.Remove(result);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}