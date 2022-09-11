using System;

namespace App.Domain.Dtos.Segment
{
    public class SegmentDtoCreateResult : BaseCreateDtoResult
    {
        public string Nome { get; set; }
        public int SubSetorId { get; set; }        
    }
}