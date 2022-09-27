using System;
using App.Domain.Enums;

namespace App.Domain.Dtos.FileImport
{
    public class FileImportDtoCreateResult
    {
        private TimeZoneInfo _curTimeZone = TimeZoneInfo.Local;
        private DateTime _dataCadastro;
        private DateTime _dataArquivo;
        private TypeFileImport _tipoArquivo;

        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string NomeArquivo { get; set; }
        public string Descricao { get; set; }

        public DateTime DataCadastro
        {
            get { return TimeZoneInfo.ConvertTimeFromUtc(_dataCadastro, _curTimeZone); }
            set { _dataCadastro = value; }
        }

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
