using System;

namespace App.Domain.Dtos.SubSector
{
    public class SubSectorDtoUpdateResult : BaseUpdateDtoResult
    {
        public string Name { get; set; }
        public int SectorId { get; set; }        
    }
}