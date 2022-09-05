using System;

namespace App.Domain.Dtos.SubSector
{
    public class SubSectorDtoUpdateResult
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int SetorId { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataAlteracao { get; set; }
    }
}