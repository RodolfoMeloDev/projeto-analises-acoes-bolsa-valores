using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Domain.Dtos
{
    public class BaseCreateDtoResult
    {
        private TimeZoneInfo _curTimeZone = TimeZoneInfo.Local;
        private DateTime _dateCreated;
        
        public int Id { get; set; }

        public DateTime DateCreated 
        { 
            get { return TimeZoneInfo.ConvertTimeFromUtc(_dateCreated, _curTimeZone); }
            set { _dateCreated = value ; } 
        }
    }
}