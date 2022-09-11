using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Domain.Dtos
{
    public class BaseDto
    {
        private TimeZoneInfo _curTimeZone = TimeZoneInfo.Local;
        private DateTime _dataCadastro;
        private DateTime? _dataAlteracao;

        public int Id { get; set; }
        public bool Ativo { get; set; }

        public DateTime DataCadastro 
        { 
            get { return TimeZoneInfo.ConvertTimeFromUtc(_dataCadastro, _curTimeZone); }
            set { _dataCadastro = value ; } 
        }

        public DateTime? DataAlteracao 
        {
            get { return _dataAlteracao != null ? TimeZoneInfo.ConvertTimeFromUtc(_dataCadastro, _curTimeZone) : null ; }
            set { _dataAlteracao = value; }
        }
    }
}