using App.Domain.Dtos.User;
using App.Domain.Models;
using AutoMapper;

namespace App.CrossCutting.Mappings
{
    public class DtoToModelProfile : Profile
    {
        public DtoToModelProfile(){
            #region User
            CreateMap<UserModel, UserDto>().ReverseMap();
            CreateMap<UserModel, UserDtoCreate>().ReverseMap();
            CreateMap<UserModel, UserDtoUpdate>().ReverseMap();
            #endregion
        }
    }
}