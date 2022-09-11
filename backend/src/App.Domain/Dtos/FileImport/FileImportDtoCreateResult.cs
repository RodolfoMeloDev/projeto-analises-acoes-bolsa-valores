using System;

namespace App.Domain.Dtos.FileImport
{
    public class FileImportDtoCreateResult
    {
        private TimeZoneInfo _curTimeZone = TimeZoneInfo.Local;
        private DateTime _dataCadastro;

        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string NomeArquivo { get; set; }
        
        public DateTime DataCadastro 
        { 
            get { return TimeZoneInfo.ConvertTimeFromUtc(_dataCadastro, _curTimeZone); }
            set { _dataCadastro = value ; } 
        }
    }
}