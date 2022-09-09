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
            return await _dataSet.FirstOrDefaultAsync(obj => obj.Nome.Equals(name) &&
                                                       obj.SubSetorId.Equals(subSectorId));
        }

        public async Task<IEnumerable<SegmentEntity>> GetAllComplete()
        {
            return await _dataSet.Include(ss => ss.SubSetor)
                .ThenInclude(s => s.Setor)
                .ToListAsync();
        }

        public async Task<SegmentEntity> GetByIdComplete(int id)
        {
            return await _dataSet.Include(ss => ss.SubSetor)
                .ThenInclude(s => s.Setor)
                .FirstOrDefaultAsync(o => o.Id.Equals(id));
        }

        public async Task<IEnumerable<SegmentEntity>> GetBySectorId(int sectorId)
        {
            return await _dataSet.Include(ss => ss.SubSetor)
                .ThenInclude(s => s.Setor)
                .Where(o => o.SubSetor.SetorId.Equals(sectorId))
                .ToListAsync();
        }

        public async Task<IEnumerable<SegmentEntity>> GetBySubSectorId(int subSectorId)
        {
            return await _dataSet.Include(ss => ss.SubSetor)
                .ThenInclude(s => s.Setor)
                .Where(o => o.SubSetorId.Equals(subSectorId))
                .ToListAsync();
        }
    }
}
