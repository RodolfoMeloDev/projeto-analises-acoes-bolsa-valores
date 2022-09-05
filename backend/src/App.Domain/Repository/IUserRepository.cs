using System.Threading.Tasks;
using App.Domain.Dtos.User;
using App.Domain.Entities;
using App.Domain.Interfaces.Services;

namespace App.Domain.Repository
{
    public interface IUserRepository : IRepository<UserEntity>
    {
        Task<UserDto> GetByLogin(string login);
        
    }
}