using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Domain.Dtos
{
    public class BaseCreateDtoResult
    {
        private TimeZoneInfo _curTimeZone = TimeZoneInfo.Local;
        private DateTime _dataCadastro;
        
        public int Id { get; set; }

        public DateTime DataCadastro 
        { 
            get { return TimeZoneInfo.ConvertTimeFromUtc(_dataCadastro, _curTimeZone); }
            set { _dataCadastro = value ; } 
        }
    }
}