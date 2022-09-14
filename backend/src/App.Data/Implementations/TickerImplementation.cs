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
    public class TickerImplementation : BaseRepository<TickerEntity>, ITickerRepository
    {
        private DbSet<TickerEntity> _dataSet;

        public TickerImplementation(AnaliseDeAcoesContext context) : base(context)
        {
            _dataSet = context.Set<TickerEntity>();
        }

        public async Task<TickerEntity> ExistTicker(string ticker)
        {
            return await _dataSet.FirstOrDefaultAsync(obj => obj.Ticker.Equals(ticker));
        }

        public async Task<IEnumerable<TickerEntity>> GetAllComplete()
        {
            return await _dataSet.Include(sg => sg.Segmento)
                .ThenInclude(ss => ss.SubSetor)
                .ThenInclude(s => s.Setor)
                .ToListAsync();
        }

        public async Task<TickerEntity> GetByIdComplete(int id)
        {
            return await _dataSet.Include(sg => sg.Segmento)
                .ThenInclude(ss => ss.SubSetor)
                .ThenInclude(s => s.Setor)
                .FirstOrDefaultAsync(obj => obj.Id.Equals(id));
        }

        public async Task<IEnumerable<TickerEntity>> GetBySectorId(int sectorId)
        {
            return await _dataSet.Include(sg => sg.Segmento)
                .ThenInclude(ss => ss.SubSetor)
                .ThenInclude(s => s.Setor)
                .Where(obj => obj.Segmento.SubSetor.SetorId.Equals(sectorId))
                .ToListAsync();
        }

        public async Task<IEnumerable<TickerEntity>> GetBySegmentoId(int segmentId)
        {
            return await _dataSet.Include(sg => sg.Segmento)
                .ThenInclude(ss => ss.SubSetor)
                .ThenInclude(s => s.Setor)
                .Where(obj => obj.SegmentoId.Equals(segmentId))
                .ToListAsync();
        }

        public async Task<IEnumerable<TickerEntity>> GetBySubSectorId(int subSectorId)
        {
            return await _dataSet.Include(sg => sg.Segmento)
                .ThenInclude(ss => ss.SubSetor)
                .ThenInclude(s => s.Setor)
                .Where(obj => obj.Segmento.SubSetorId.Equals(subSectorId))
                .ToListAsync();
        }

        public async Task<TickerEntity> GetByTickerComplete(string ticker)
        {
            return await _dataSet.Include(sg => sg.Segmento)
                .ThenInclude(ss => ss.SubSetor)
                .ThenInclude(s => s.Setor)
                .FirstOrDefaultAsync(obj => obj.Ticker.Equals(ticker));
        }
    }
}