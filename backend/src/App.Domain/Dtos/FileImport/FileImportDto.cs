using System;
using App.Domain.Dtos.User;

namespace App.Domain.Dtos.FileImport
{
    public class FileImportDto : BaseDto
    {
        private TimeZoneInfo _curTimeZone = TimeZoneInfo.Local;
        private DateTime _dataArquivo;

        public int UsuarioId { get; set; }
        public string NomeArquivo { get; set; }
        public string Descricao { get; set; }

        public DateTime DataArquivo 
        { 
            get { return TimeZoneInfo.ConvertTimeFromUtc(_dataArquivo, _curTimeZone); }
            set { _dataArquivo = value ; } 
        }
    }
}