using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Data.Context;
using App.Data.Repository;
using App.Data.Repository.Exceptions;
using App.Domain.Entities;
using App.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace App.Data.Implementations
{
    public class SubSectorImplementation : BaseRepository<SubSectorEntity>, ISubSectorRepository
    {
        private DbSet<SubSectorEntity> _dataSet;

        public SubSectorImplementation(StockAnalysisContext context) : base(context)
        {
            _dataSet = context.Set<SubSectorEntity>();
        }

        public async Task<SubSectorEntity> ExistSubSector(string name, int sectorId)
        {
            return await _dataSet.FirstOrDefaultAsync(obj => obj.Name.Equals(name.ToUpper()) &&
                                                             obj.SectorId.Equals(sectorId));
        }

        public async Task<IEnumerable<SubSectorEntity>> GetBySectorId(int sectorId)
        {
            return await _dataSet.Include(s => s.Sector)
                                 .Where(s => s.SectorId.Equals(sectorId))
                                 .ToListAsync();
        }

        public async Task<SubSectorEntity> GetByIdComplete(int id)
        {
            try
            {
                var result = await _dataSet.Include(s => s.Sector)
                                           .FirstOrDefaultAsync(obj => obj.Id.Equals(id));

                if (result == null)
                    throw new IntegrityException("ID não encontrado");

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<SubSectorEntity>> GetAllComplete()
        {
            try
            {
                return await _dataSet.Include(s => s.Sector)
                                     .ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
