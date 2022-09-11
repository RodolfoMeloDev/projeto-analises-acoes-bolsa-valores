using System;

namespace App.Domain.Dtos.Segment
{
    public class SegmentDtoUpdateResult : BaseUpdateDtoResult
    {
        public string Nome { get; set; }
        public int SubSetorId { get; set; }        
    }
}