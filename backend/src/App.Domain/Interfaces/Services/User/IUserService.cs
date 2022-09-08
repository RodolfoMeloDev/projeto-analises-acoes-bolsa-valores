using System.Collections.Generic;
using System.Threading.Tasks;
using App.Domain.Dtos.User;

namespace App.Domain.Interfaces.Services.User
{
    public interface IUserService
    {
        Task<UserDto> GetUserById(int id);
        Task<UserDto> GetUserByLogin(string login);
        Task<IEnumerable<UserDto>> GetAllUsers();
        Task<UserDtoCreateResult> InsertUser(UserDtoCreate user);
        Task<UserDtoUpdateResult> UpdateUser(UserDtoUpdate user);
        Task<bool> DeleteUser(int id);
    }
}