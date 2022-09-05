using System;

namespace App.Domain.Dtos.Segment
{
    public class SegmentDtoUpdateResult
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int SubSetorId { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataAlteracao { get; set; }
    }
}