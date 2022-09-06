using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Domain.Dtos.User;
using App.Domain.Entities;
using App.Domain.Interfaces.Services;
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

        public async Task<UserDto> GetLogin(string login)
        {
            var entity = await _repository.GetByLogin(login);

            return _mapper.Map<UserDto>(entity);
        }

        public async Task<UserDto> GetUser(int id)
        {
            var entity = await _repository.SelectAsync(id);

            return _mapper.Map<UserDto>(entity);
        }

        public async Task<IEnumerable<UserDto>> GetUsers()
        {
            var listEntity = await _repository.SelectAllAsync();

            return _mapper.Map<IEnumerable<UserDto>>(listEntity);
        }

        public async Task<UserDtoCreateResult> InsertUser(UserDtoCreate user)
        {
            var existLogin = await _repository.GetByLogin(user.Login);

            if (existLogin == null){
                var model = _mapper.Map<UserModel>(user);
                var entity = _mapper.Map<UserEntity>(model);
                var result = await _repository.InsertAsync(entity);

                return _mapper.Map<UserDtoCreateResult>(result);
            }else{
                throw new IntegrityException("Login já cadastrado!");
            }
        }

        public async Task<UserDtoUpdateResult> UpdateUser(UserDtoUpdate user)
        {
            var model = _mapper.Map<UserModel>(user);
            var entity = _mapper.Map<UserEntity>(model);
            var result = await _repository.UpdateAsync(entity);

            return _mapper.Map<UserDtoUpdateResult>(result);
        }

        public async Task<bool> DeleteUser(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}