using System;

namespace App.Domain.Dtos.Segment
{
    public class SegmentDtoCreateResult : BaseCreateDtoResult
    {
        public string Name { get; set; }
        public int SubSectorId { get; set; }        
    }
}