using System.Threading.Tasks;
using App.Data.Context;
using App.Data.Repository;
using App.Domain.Entities;
using App.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace App.Data.Implementations
{
    public class SectorImplementation : BaseRepository<SectorEntity>, ISectorRepository
    {
        private DbSet<SectorEntity> _dataSet;

        public SectorImplementation(StockAnalysisContext context) : base(context)
        {
            _dataSet = context.Set<SectorEntity>();
        }

        public async Task<SectorEntity> GetByName(string name)
        {
            return await _dataSet.FirstOrDefaultAsync(obj => obj.Name.Equals(name.ToUpper()));
        }
    }
}
