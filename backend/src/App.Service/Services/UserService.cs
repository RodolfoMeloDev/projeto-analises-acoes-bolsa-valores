using App.Domain.Dtos.User;
using App.Domain.Entities;
using App.Domain.Interfaces.Services.User;
using App.Domain.Models;
using App.Domain.Repository;
using App.Service.Services.Exceptions;
using AutoMapper;

namespace App.Service.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _repository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<UserDto> GetUserByLogin(string login)
        {
            var entity = await _repository.GetByLogin(login);

            return _mapper.Map<UserDto>(entity);
        }

        public async Task<UserDto> GetUserById(int id)
        {
            var entity = await _repository.SelectAsync(id);

            return _mapper.Map<UserDto>(entity);
        }

        public async Task<UserDtoCreateResult> Insert(UserDtoCreate user)
        {
            var existLogin = await _repository.GetByLogin(user.Login);

            if (existLogin == null)
            {
                var model = _mapper.Map<UserModel>(user);
                var entity = _mapper.Map<UserEntity>(model);
                var result = await _repository.InsertAsync(entity);

                return _mapper.Map<UserDtoCreateResult>(result);
            }

            throw new IntegrityException("Login já cadastrado!");
        }

        public async Task<UserDtoUpdateResult> Update(UserDtoUpdate user)
        {
            var oldUser = await _repository.SelectAsync(user.Id);

            if (string.IsNullOrEmpty(user.NickName))
                user.NickName = oldUser.NickName;

            var model = _mapper.Map<UserModel>(user);
            var entity = _mapper.Map<UserEntity>(model);
            var result = await _repository.UpdateAsync(entity);

            return _mapper.Map<UserDtoUpdateResult>(result);
        }

        public async Task<bool> Delete(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
