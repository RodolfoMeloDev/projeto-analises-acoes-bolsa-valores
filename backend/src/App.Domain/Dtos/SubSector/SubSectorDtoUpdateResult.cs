using System;

namespace App.Domain.Dtos.SubSector
{
    public class SubSectorDtoUpdateResult : BaseUpdateDtoResult
    {
        public string Nome { get; set; }
        public int SetorId { get; set; }        
    }
}