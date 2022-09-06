using App.Domain.Dtos.User;
using App.Domain.Entities;
using AutoMapper;

namespace App.CrossCutting.Mappings
{
    public class EntityToDtoProfile : Profile
    {
        public EntityToDtoProfile(){
            # region User
            CreateMap<UserDto, UserEntity>().ReverseMap();
            CreateMap<UserDtoCreateResult, UserEntity>().ReverseMap();
            CreateMap<UserDtoUpdateResult, UserEntity>().ReverseMap();
            # endregion
        }
    }
}