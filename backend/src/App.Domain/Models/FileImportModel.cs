using System;

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
            set { _nomeArquivo = value; }
        }
        
        private string _descricao;
        public string Descricao
        {
            get { return _descricao; }
            set { _descricao = value; }
        }

        private DateTime _dataArquivo;
        public DateTime DataArquivo
        {
            get { return _dataArquivo; }
            set { _dataArquivo = value; }
        }
           
    }
}