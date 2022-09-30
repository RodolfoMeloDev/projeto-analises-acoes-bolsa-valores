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
    public class SegmentImplementation : BaseRepository<SegmentEntity>, ISegmentRepository
    {
        private DbSet<SegmentEntity> _dataSet;

        public SegmentImplementation(AnaliseDeAcoesContext context) : base(context)
        {
            _dataSet = context.Set<SegmentEntity>();
        }

        public async Task<SegmentEntity> ExistSegment(string name, int subSectorId)
        {
            return await _dataSet.FirstOrDefaultAsync(obj => obj.Name.Equals(name.ToUpper()) &&
                                                             obj.SubSectorId.Equals(subSectorId));
        }

        public async Task<IEnumerable<SegmentEntity>> GetAllComplete()
        {
            return await _dataSet.Include(ss => ss.SubSector)
                .ThenInclude(s => s.Sector)
                .ToListAsync();
        }

        public async Task<SegmentEntity> GetByIdComplete(int id)
        {
            return await _dataSet.Include(ss => ss.SubSector)
                .ThenInclude(s => s.Sector)
                .FirstOrDefaultAsync(o => o.Id.Equals(id));
        }

        public async Task<IEnumerable<SegmentEntity>> GetBySectorId(int sectorId)
        {
            return await _dataSet.Include(ss => ss.SubSector)
                .ThenInclude(s => s.Sector)
                .Where(o => o.SubSector.SectorId.Equals(sectorId))
                .ToListAsync();
        }

        public async Task<IEnumerable<SegmentEntity>> GetBySubSectorId(int subSectorId)
        {
            return await _dataSet.Include(ss => ss.SubSector)
                .ThenInclude(s => s.Sector)
                .Where(o => o.SubSectorId.Equals(subSectorId))
                .ToListAsync();
        }
    }
}
