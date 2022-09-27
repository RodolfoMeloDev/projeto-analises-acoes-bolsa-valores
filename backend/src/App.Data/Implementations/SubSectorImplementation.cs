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

        public SubSectorImplementation(AnaliseDeAcoesContext context) : base(context)
        {
            _dataSet = context.Set<SubSectorEntity>();
        }

        public async Task<SubSectorEntity> ExistSubSector(string name, int sectorId)
        {
            return await _dataSet.FirstOrDefaultAsync(obj => obj.Nome.Equals(name.ToUpper()) &&
                                                             obj.SetorId.Equals(sectorId));
        }

        public async Task<IEnumerable<SubSectorEntity>> GetBySectorId(int sectorId)
        {
            return await _dataSet.Include(s => s.Setor)
                .Where(s => s.SetorId.Equals(sectorId))
                .ToListAsync();
        }

        public async Task<SubSectorEntity> GetByIdComplete(int id)
        {
            try
            {
                var result = await _dataSet.Include(s => s.Setor)
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
                return await _dataSet.Include(s => s.Setor)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
