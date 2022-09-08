using System.Threading.Tasks;
using App.Domain.Entities;
using App.Domain.Interfaces.Services;

namespace App.Domain.Repository
{
    public interface IUserRepository : IRepository<UserEntity>
    {
        Task<UserEntity> GetByLogin(string login);        
    }
}