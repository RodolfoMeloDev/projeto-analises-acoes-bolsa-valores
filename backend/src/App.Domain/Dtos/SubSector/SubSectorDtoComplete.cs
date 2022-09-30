using System;
using App.Domain.Dtos.Sector;

namespace App.Domain.Dtos.SubSector
{
    public class SubSectorDtoComplete : BaseDto
    {
        public string Name { get; set; }
        public int SectorId { get; set; }
        public SectorDto Sector { get; set; }        
    }
}