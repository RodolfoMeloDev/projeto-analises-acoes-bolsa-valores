using System;

namespace App.Domain.Dtos.SubSector
{
    public class SubSectorDtoCreateResult : BaseCreateDtoResult
    {
        public string Name { get; set; }
        public int SectorId { get; set; }        
    }
}