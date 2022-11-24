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
    public class BaseTickerImplementation : BaseRepository<BaseTickerEntity>, IBaseTickerRepository
    {
        private DbSet<BaseTickerEntity> _dataSet;

        public BaseTickerImplementation(StockAnalysisContext context) : base(context)
        {
            _dataSet = context.Set<BaseTickerEntity>();
        }

        public async Task<IEnumerable<BaseTickerEntity>> GetAllBySegment(int segment){
            return await _dataSet.Include(x => x.Segment)
                                 .ThenInclude(x => x.SubSector)
                                 .ThenInclude(x => x.Sector)
                                 .Where(x => x.SegmentId.Equals(segment))
                                 .ToListAsync();
        }

        public async Task<IEnumerable<BaseTickerEntity>> GetAllBySubSector(int subSector){
            return await _dataSet.Include(x => x.Segment)
                                 .ThenInclude(x => x.SubSector)
                                 .ThenInclude(x => x.Sector)
                                 .Where(x => x.Segment.SubSectorId.Equals(subSector))
                                 .ToListAsync();
        }

        public async Task<IEnumerable<BaseTickerEntity>> GetAllBySector(int sector){
            return await _dataSet.Include(x => x.Segment)
                                 .ThenInclude(x => x.SubSector)
                                 .ThenInclude(x => x.Sector)
                                 .Where(x => x.Segment.SubSector.SectorId.Equals(sector))
                                 .ToListAsync();
        }

        public async Task<IEnumerable<BaseTickerEntity>> GetAllComplete()
        {
            return await _dataSet.Include(x => x.Segment)
                                 .ThenInclude(x => x.SubSector)
                                 .ThenInclude(x => x.Sector)
                                 .ToListAsync();
        }
    }
}