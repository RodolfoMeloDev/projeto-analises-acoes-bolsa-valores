using App.Domain.Dtos.FileImport;
using App.Domain.Dtos.Sector;
using App.Domain.Dtos.Segment;
using App.Domain.Dtos.SubSector;
using App.Domain.Dtos.Ticker;
using App.Domain.Dtos.User;
using App.Domain.Models;
using AutoMapper;

namespace App.CrossCutting.Mappings
{
    public class DtoToModelProfile : Profile
    {
        public DtoToModelProfile()
        {
            #region User
            CreateMap<UserModel, UserDto>().ReverseMap();
            CreateMap<UserModel, UserDtoCreate>().ReverseMap();
            CreateMap<UserModel, UserDtoUpdate>().ReverseMap();
            #endregion

            #region Sector
            CreateMap<SectorModel, SectorDto>().ReverseMap();
            CreateMap<SectorModel, SectorDtoCreate>().ReverseMap();
            CreateMap<SectorModel, SectorDtoUpdate>().ReverseMap();
            #endregion

            #region SubSector
            CreateMap<SubSectorModel, SubSectorDto>().ReverseMap();
            CreateMap<SubSectorModel, SubSectorDtoCreate>().ReverseMap();
            CreateMap<SubSectorModel, SubSectorDtoUpdate>().ReverseMap();
            #endregion

            #region  Segment
            CreateMap<SegmentModel, SegmentDto>().ReverseMap();
            CreateMap<SegmentModel, SegmentDtoCreate>().ReverseMap();
            CreateMap<SegmentModel, SegmentDtoUpdate>().ReverseMap();
            #endregion

            #region Ticker
            CreateMap<TickerModel, TickerDto>().ReverseMap();
            CreateMap<TickerModel, TickerDtoCreate>().ReverseMap();
            CreateMap<TickerModel, TickerDtoUpdate>().ReverseMap();
            #endregion

            #region  File Import
            CreateMap<FileImportModel, FileImportDto>().ReverseMap();
            CreateMap<FileImportModel, FileImportDtoCreate>().ReverseMap();
            #endregion
        }
    }
}
