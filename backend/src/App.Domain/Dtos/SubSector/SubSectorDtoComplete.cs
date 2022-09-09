using System;
using App.Domain.Dtos.Sector;

namespace App.Domain.Dtos.SubSector
{
    public class SubSectorDtoComplete
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int SetorId { get; set; }
        public SectorDto Setor { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataAlteracao { get; set; }
    }
}