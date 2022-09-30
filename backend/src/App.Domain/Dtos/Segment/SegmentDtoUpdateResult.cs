using System;

namespace App.Domain.Dtos.Segment
{
    public class SegmentDtoUpdateResult : BaseUpdateDtoResult
    {
        public string Name { get; set; }
        public int SubSectorId { get; set; }        
    }
}