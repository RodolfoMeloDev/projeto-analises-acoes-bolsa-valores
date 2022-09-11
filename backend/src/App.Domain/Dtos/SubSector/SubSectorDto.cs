using System;
using App.Domain.Dtos.Sector;

namespace App.Domain.Dtos.SubSector
{
    public class SubSectorDto : BaseDto
    {
        public string Nome { get; set; }
        public int SetorId { get; set; }        
    }
}