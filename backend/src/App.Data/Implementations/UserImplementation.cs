using System.Threading.Tasks;
using App.Data.Context;
using App.Data.Repository;
using App.Domain.Entities;
using App.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace App.Data.Implementations
{
    public class UserImplementation : BaseRepository<UserEntity>, IUserRepository
    {
        private DbSet<UserEntity> _dataSet;

        public UserImplementation(StockAnalysisContext context) : base(context)
        {
            _dataSet = _context.Set<UserEntity>();
        }

        public async Task<UserEntity> GetByLogin(string login)
        {
            return await _dataSet.FirstOrDefaultAsync(obj => obj.Login.Equals(login.ToUpper()));
        }
    }
}