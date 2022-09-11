using System;

namespace App.Domain.Dtos.Segment
{
    public class SegmentDto : BaseDto
    {
        public string Nome { get; set; }
        public int SubSetorId { get; set; }        
    }
}
