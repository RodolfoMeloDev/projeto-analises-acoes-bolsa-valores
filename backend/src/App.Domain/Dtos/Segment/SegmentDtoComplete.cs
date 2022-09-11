using System;
using App.Domain.Dtos.SubSector;

namespace App.Domain.Dtos.Segment
{
    public class SegmentDtoComplete : BaseDto
    {
        public string Nome { get; set; }
        public int SubSetorId { get; set; }
        public SubSectorDtoComplete SubSetor { get; set; }        
    }
}
