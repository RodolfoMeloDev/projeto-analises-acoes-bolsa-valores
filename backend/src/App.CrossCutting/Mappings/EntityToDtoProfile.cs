using App.Domain.Dtos.FileImport;
using App.Domain.Dtos.Sector;
using App.Domain.Dtos.Segment;
using App.Domain.Dtos.SubSector;
using App.Domain.Dtos.Ticker;
using App.Domain.Dtos.User;
using App.Domain.Entities;
using AutoMapper;

namespace App.CrossCutting.Mappings
{
    public class EntityToDtoProfile : Profile
    {
        public EntityToDtoProfile()
        {
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

            #region Segment
            CreateMap<SegmentDto, SegmentEntity>().ReverseMap();
            CreateMap<SegmentDtoComplete, SegmentEntity>().ReverseMap();
            CreateMap<SegmentDtoCreateResult, SegmentEntity>().ReverseMap();
            CreateMap<SegmentDtoUpdateResult, SegmentEntity>().ReverseMap();
            #endregion

            #region Ticker
            CreateMap<TickerDto, TickerEntity>().ReverseMap();
            CreateMap<TickerDtoComplete, TickerEntity>().ReverseMap();
            CreateMap<TickerDtoCreateResult, TickerEntity>().ReverseMap();
            CreateMap<TickerDtoUpdateResult, TickerEntity>().ReverseMap();
            #endregion

            #region File Import
            CreateMap<FileImportDto, FileImportEntity>().ReverseMap();
            CreateMap<FileImportDtoCreateResult, FileImportEntity>().ReverseMap();
            #endregion
        }
    }
}
