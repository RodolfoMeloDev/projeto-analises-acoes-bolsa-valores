using System;
using App.Domain.Dtos.Sector;

namespace App.Domain.Dtos.SubSector
{
    public class SubSectorDto : BaseDto
    {
        public string Name { get; set; }
        public int SectorId { get; set; }        
    }
}