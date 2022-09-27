using System;
using App.Domain.Enums;

namespace App.Domain.Models
{
    public class FileImportModel : BaseModel
    {
        private int _usuarioId;
        public int UsuarioId
        {
            get { return _usuarioId; }
            set { _usuarioId = value; }
        }

        private string _nomeArquivo;
        public string NomeArquivo
        {
            get { return _nomeArquivo; }
            set { _nomeArquivo = (string.IsNullOrEmpty(value) ? null : value.ToUpper()); }
        }

        private string _descricao;
        public string Descricao
        {
            get { return _descricao; }
            set { _descricao = (string.IsNullOrEmpty(value) ? null : value.ToUpper()); }
        }

        private DateTime _dataArquivo;
        public DateTime DataArquivo
        {
            get { return _dataArquivo; }
            set { _dataArquivo = value; }
        }

        private TypeFileImport _tipoArquivo;
        public TypeFileImport TipoArquivo
        {
            get { return _tipoArquivo; }
            set { _tipoArquivo = value; }
        }


    }
}
