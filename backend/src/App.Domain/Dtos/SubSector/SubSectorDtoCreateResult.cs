using System;

namespace App.Domain.Dtos.SubSector
{
    public class SubSectorDtoCreateResult
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int SubSectorId { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}