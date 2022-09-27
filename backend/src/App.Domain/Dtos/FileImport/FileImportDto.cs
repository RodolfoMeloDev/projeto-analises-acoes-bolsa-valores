using System;
using App.Domain.Dtos.User;
using App.Domain.Enums;

namespace App.Domain.Dtos.FileImport
{
    public class FileImportDto : BaseDto
    {
        private TimeZoneInfo _curTimeZone = TimeZoneInfo.Local;
        private DateTime _dataArquivo;
        private TypeFileImport _tipoArquivo;

        public int UsuarioId { get; set; }
        public string NomeArquivo { get; set; }
        public string Descricao { get; set; }

        public DateTime DataArquivo
        {
            get { return TimeZoneInfo.ConvertTimeFromUtc(_dataArquivo, _curTimeZone); }
            set { _dataArquivo = value; }
        }

        public TypeFileImport TipoArquivo
        {
            get { return _tipoArquivo; }
            set { _tipoArquivo = value; }
        }

        public string NomeTipoArquivo
        {
            get { return Enum.GetName(typeof(TypeFileImport), _tipoArquivo); }
        }
    }
}
