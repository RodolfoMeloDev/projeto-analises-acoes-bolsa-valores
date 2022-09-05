using System;

namespace App.Domain.Dtos.Segment
{
    public class SegmentDtoCreateResult
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int SubSetorId { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}