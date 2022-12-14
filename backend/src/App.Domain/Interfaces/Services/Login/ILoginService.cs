using System.Threading.Tasks;
using App.Domain.Dtos.Login;

namespace App.Domain.Interfaces.Services.Login
{
    public interface ILoginService
    {
        Task<object> FindByLogin(LoginDto user);
        Task<object> RefreshToken(LoginDtoRefreshTokenUpdate refreshToken);
    }
}
