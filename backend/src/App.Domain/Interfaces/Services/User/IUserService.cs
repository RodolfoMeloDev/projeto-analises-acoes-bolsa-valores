using System.Collections.Generic;
using System.Threading.Tasks;
using App.Domain.Dtos.User;

namespace App.Domain.Interfaces.Services.User
{
    public interface IUserService
    {
        Task<UserDto> GetUser(int id);
        Task<UserDto> GetLogin(string login);
        Task<IEnumerable<UserDto>> GetUsers();
        Task<UserDtoCreateResult> InsertUser(UserDtoCreate user);
        Task<UserDtoUpdateResult> UpdateUser(UserDtoUpdate user);
        Task<bool> DeleteUser(int id);
    }
}