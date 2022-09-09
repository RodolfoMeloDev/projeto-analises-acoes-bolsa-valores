using App.Domain.Dtos.Sector;
using App.Domain.Dtos.SubSector;
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

            #region Sector
            CreateMap<SectorDto, SectorEntity>().ReverseMap();
            CreateMap<SectorDtoCreateResult, SectorEntity>().ReverseMap();
            CreateMap<SectorDtoUpdateResult, SectorEntity>().ReverseMap();
            #endregion

            #region SubSector
            CreateMap<SubSectorDto, SubSectorEntity>().ReverseMap();
            CreateMap<SubSectorDtoComplete, SubSectorEntity>().ReverseMap();
            CreateMap<SubSectorDtoCreateResult, SubSectorEntity>().ReverseMap();
            CreateMap<SubSectorDtoUpdateResult, SubSectorEntity>().ReverseMap();
            #endregion
        }
    }
}