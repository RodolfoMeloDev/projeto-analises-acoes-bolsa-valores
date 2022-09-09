using App.Domain.Entities;
using App.Domain.Models;
using AutoMapper;

namespace App.CrossCutting.Mappings
{
    public class ModelToEntityProfile : Profile
    {
        public ModelToEntityProfile(){
            CreateMap<UserEntity, UserModel>().ReverseMap();
            CreateMap<SectorEntity, SectorModel>().ReverseMap();
            CreateMap<SubSectorEntity, SubSectorModel>().ReverseMap();
        }
    }
}