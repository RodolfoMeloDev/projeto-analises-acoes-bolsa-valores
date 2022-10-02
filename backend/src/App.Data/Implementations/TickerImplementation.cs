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

        public TickerImplementation(StockAnalysisContext context) : base(context)
        {
            _dataSet = context.Set<TickerEntity>();
        }

        public async Task<TickerEntity> ExistTicker(string ticker)
        {
            return await _dataSet.Include(bt => bt.BaseTicker)
                                 .FirstOrDefaultAsync(obj => obj.Ticker.Equals(ticker.ToUpper()));
        }

        public async Task<IEnumerable<TickerEntity>> GetAllComplete()
        {
            return await _dataSet.Include(bt => bt.BaseTicker)
                                 .ThenInclude(sg => sg.Segment)
                                 .ThenInclude(ss => ss.SubSector)
                                 .ThenInclude(s => s.Sector)
                                 .ToListAsync();
        }

        public async Task<TickerEntity> GetByIdComplete(int id)
        {
            return await _dataSet.Include(bt => bt.BaseTicker)
                                 .ThenInclude(sg => sg.Segment)
                                 .ThenInclude(ss => ss.SubSector)
                                 .ThenInclude(s => s.Sector)
                                 .FirstOrDefaultAsync(obj => obj.Id.Equals(id));
        }

        public async Task<IEnumerable<TickerEntity>> GetBySectorId(int sectorId)
        {
            return await _dataSet.Include(bt => bt.BaseTicker)
                                 .ThenInclude(sg => sg.Segment)
                                 .ThenInclude(ss => ss.SubSector)
                                 .ThenInclude(s => s.Sector)
                                 .Where(obj => obj.BaseTicker.Segment.SubSector.SectorId.Equals(sectorId))
                                 .ToListAsync();
        }

        public async Task<IEnumerable<TickerEntity>> GetBySegmentoId(int segmentId)
        {
            return await _dataSet.Include(bt => bt.BaseTicker)
                                 .ThenInclude(sg => sg.Segment)
                                 .ThenInclude(ss => ss.SubSector)
                                 .ThenInclude(s => s.Sector)
                                 .Where(obj => obj.BaseTicker.SegmentId.Equals(segmentId))
                                 .ToListAsync();
        }

        public async Task<IEnumerable<TickerEntity>> GetBySubSectorId(int subSectorId)
        {
            return await _dataSet.Include(bt => bt.BaseTicker)
                                 .ThenInclude(sg => sg.Segment)
                                 .ThenInclude(ss => ss.SubSector)
                                 .ThenInclude(s => s.Sector)
                                 .Where(obj => obj.BaseTicker.Segment.SubSectorId.Equals(subSectorId))
                                 .ToListAsync();
        }

        public async Task<TickerEntity> GetByTickerComplete(string ticker)
        {
            return await _dataSet.Include(bt => bt.BaseTicker)
                                 .ThenInclude(sg => sg.Segment)
                                 .ThenInclude(ss => ss.SubSector)
                                 .ThenInclude(s => s.Sector)
                                 .FirstOrDefaultAsync(obj => obj.Ticker.Equals(ticker.ToUpper()));
        }
    }
}
