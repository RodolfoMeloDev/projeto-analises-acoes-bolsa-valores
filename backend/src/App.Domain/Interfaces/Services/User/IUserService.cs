using System.Collections.Generic;
using System.Threading.Tasks;
using App.Domain.Dtos.User;

namespace App.Domain.Interfaces.Services.User
{
    public interface IUserService
    {
        Task<UserDto> GetUserByLogin(string login);
        Task<UserDtoCreateResult> Insert(UserDtoCreate user);
        Task<UserDtoUpdateResult> Update(UserDtoUpdate user);
        Task<bool> Delete(int id);
    }
}
