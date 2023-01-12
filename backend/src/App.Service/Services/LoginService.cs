using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using App.Domain.Dtos.Login;
using App.Domain.Entities;
using App.Domain.Interfaces.Services.Login;
using App.Domain.Repository;
using App.Domain.Security;
using App.Service.Services.Exceptions;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace App.Service.Services
{
    public class LoginService : ILoginService
    {
        private IUserRepository _repository;
        private SigningConfigurations _signingConfigurations;
        private IConfiguration _configuration;

        public LoginService(IUserRepository repository, SigningConfigurations signingConfigurations, IConfiguration configuration)
        {
            _repository = repository;
            _signingConfigurations = signingConfigurations;
            _configuration = configuration;
        }

        public async Task<object> FindByLogin(LoginDto user)
        {
            var baseUser = new UserEntity();

            if (user != null && !string.IsNullOrWhiteSpace(user.Login))
            {
                baseUser = await _repository.GetByLogin(user.Login);

                if (baseUser == null)
                {
                    return new
                    {
                        authenticated = false,
                        message = "Falha ao autenticar: Login n�o encontrado!"
                    };
                }
                else
                {
                    if (!baseUser.Password.Equals(user.Password))
                    {
                        return new
                        {
                            authenticated = false,
                            message = "Falha ao autenticar: Senha inv�lida!"
                        };
                    }
                }

                ClaimsIdentity identity = new ClaimsIdentity(new GenericIdentity(baseUser.Login),
                    new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.UniqueName, user.Login)
                    }
                    );

                DateTime createDate = DateTime.Now;
                DateTime expirationDate = createDate + TimeSpan.FromSeconds(Convert.ToInt32(Environment.GetEnvironmentVariable("TokenSeconds")));

                var handler = new JwtSecurityTokenHandler();

                string token = CreateToken(identity, createDate, expirationDate, handler);
                string refreshToken = GenerateRefreshToken();
                DateTime refreshTokenexpirationDate = createDate + TimeSpan.FromSeconds(Convert.ToInt32(Environment.GetEnvironmentVariable("RefreshTokenSeconds")));

                await _repository.UpdateRefreshToken(baseUser.Id, refreshToken, refreshTokenexpirationDate);

                return SuccessObject(createDate, refreshTokenexpirationDate, token, baseUser, refreshToken);
            }

            return new
            {
                authenticated = false,
                message = "Falha ao autenticar"
            };
        }

        public async Task<object> RefreshToken(LoginDtoRefreshTokenUpdate refreshToken)
        {
            var user = await _repository.GetByLogin(refreshToken.Login);

            if (user == null)
                throw new IntegrityException("Login não encontrado");

            if (user.RefreshToken != refreshToken.RefreshToken) 
                throw new IntegrityException("RefreshToken inválido!");

            if (DateTime.UtcNow >= user.RefreshTokenExpiration)
                throw new IntegrityException("RefreshToken expirado!");

            ClaimsIdentity identity = new ClaimsIdentity(new GenericIdentity(user.Login),
                    new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.UniqueName, user.Login)
                    }
                    );

            DateTime createDate = DateTime.Now;
            DateTime expirationDate = createDate + TimeSpan.FromSeconds(Convert.ToInt32(Environment.GetEnvironmentVariable("TokenSeconds")));

            var handler = new JwtSecurityTokenHandler();

            string newToken = CreateToken(identity, createDate, expirationDate, handler);
            string newRefreshToken = GenerateRefreshToken();
            DateTime refreshTokenexpirationDate = createDate + TimeSpan.FromSeconds(Convert.ToInt32(Environment.GetEnvironmentVariable("RefreshTokenSeconds")));

            var result = await _repository.UpdateRefreshToken(user.Id, newRefreshToken, refreshTokenexpirationDate);

            return new
            {
                login = user.Login,
                expiration = refreshTokenexpirationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                accessToken = newToken,
                refreshToken = newRefreshToken,
            };
        }

        private string CreateToken(ClaimsIdentity identity, DateTime createDate, DateTime expirationDate, JwtSecurityTokenHandler handler)
        {
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = Environment.GetEnvironmentVariable("Issuer"),
                Audience = Environment.GetEnvironmentVariable("Audience"),
                SigningCredentials = _signingConfigurations.SigningCredentials,
                Subject = identity,
                NotBefore = createDate,
                Expires = expirationDate
            });

            var token = handler.WriteToken(securityToken);
            return token;
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private object SuccessObject(DateTime createDate, DateTime expirationDate, string token, UserEntity user, string refreshToken)
        {
            return new
            {
                authenticated = true,
                created = createDate.ToString("yyyy-MM-dd HH:mm:ss"),
                expiration = expirationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                accessToken = token,
                refreshToken = refreshToken,
                Login = user.Login,
                Name = user.Name,
                NickName = user.NickName,
                message = "Usuário Logado com sucesso"
            };
        }
    }
}
