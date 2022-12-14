using System;
using System.Threading.Tasks;
using App.Domain.Entities;
using App.Domain.Interfaces.Services;

namespace App.Domain.Repository
{
    public interface IUserRepository : IRepository<UserEntity>
    {
        Task<UserEntity> GetByLogin(string login);
        Task<UserEntity> UpdateRefreshToken(int id, string refreshToken, DateTime expirationRefreshToken);
    }
}
