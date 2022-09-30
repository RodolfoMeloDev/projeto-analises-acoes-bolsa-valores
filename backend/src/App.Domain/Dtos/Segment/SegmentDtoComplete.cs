using System;
using App.Domain.Dtos.SubSector;

namespace App.Domain.Dtos.Segment
{
    public class SegmentDtoComplete : BaseDto
    {
        public string Name { get; set; }
        public int SubSectorId { get; set; }
        public SubSectorDtoComplete SubSector { get; set; }        
    }
}
