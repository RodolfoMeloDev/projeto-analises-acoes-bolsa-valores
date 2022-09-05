using System;

namespace App.Domain.Dtos.Sector
{
    public class SectorDtoCreateResult
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}