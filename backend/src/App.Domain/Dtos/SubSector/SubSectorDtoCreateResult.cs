using System;

namespace App.Domain.Dtos.SubSector
{
    public class SubSectorDtoCreateResult : BaseCreateDtoResult
    {
        public string Nome { get; set; }
        public int SetorId { get; set; }        
    }
}