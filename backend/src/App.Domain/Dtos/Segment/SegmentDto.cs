using System;

namespace App.Domain.Dtos.Segment
{
    public class SegmentDto : BaseDto
    {
        public string Name { get; set; }
        public int SubSectorId { get; set; }        
    }
}
