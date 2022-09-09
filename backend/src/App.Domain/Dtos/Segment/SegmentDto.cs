using System;

namespace App.Domain.Dtos.Segment
{
    public class SegmentDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int SubSetorId { get; set; }
        public bool Status { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataAlteracao { get; set; }
    }
}
