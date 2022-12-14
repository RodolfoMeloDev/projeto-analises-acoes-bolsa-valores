using System;
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

        public async Task<UserEntity> UpdateRefreshToken(int id, string refreshToken, DateTime expirationRefreshToken)
        {
            try
            {
                if (expirationRefreshToken.Kind != DateTimeKind.Utc)
                    expirationRefreshToken = TimeZoneInfo.ConvertTimeToUtc(expirationRefreshToken);

                var result = await SelectAsync(id);

                result.DateUpdated = DateTime.UtcNow;
                result.RefreshToken = refreshToken;

                result.RefreshTokenExpiration = expirationRefreshToken;

                _context.Entry(result).CurrentValues.SetValues(result);

                await _context.SaveChangesAsync();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
