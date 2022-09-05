using System;

namespace App.Domain.Models
{
    public class BaseModel
    {
        private int _id;
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private bool _ativo;
        public bool MyProperty
        {
            get { return _ativo; }
            set { _ativo = value; }
        }
        
        private DateTime _dataCriacao;
        public DateTime DataCriacao
        {
            get { return _dataCriacao; }
            set { _dataCriacao = value == null ? DateTime.UtcNow : value; }
        }

        private DateTime _dataAlteracao;
        public DateTime DataAlteracao
        {
            get { return _dataAlteracao; }
            set { _dataAlteracao = value; }
        }                
    }
}