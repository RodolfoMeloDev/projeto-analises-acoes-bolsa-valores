using System;
using App.Domain.Dtos.Sector;

namespace App.Domain.Dtos.SubSector
{
    public class SubSectorDtoComplete : BaseDto
    {
        public string Nome { get; set; }
        public int SetorId { get; set; }
        public SectorDto Setor { get; set; }        
    }
}