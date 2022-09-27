using System;

namespace App.Domain.Models.DataTicker
{
    public class DataTickerModel
    {
        private string _cd_acao_rdz { get; set; }
        private string _nm_empresa { get; set; }
        private string _setor_economico { get; set; }
        private string _subsetor { get; set; }
        private string _segmento { get; set; }
        private string _segmento_b3 { get; set; }
        private string _nm_segmento_b3 { get; set; }
        private string _cd_acao { get; set; }
        private string _tx_cnpj { get; set; }
        private string _vl_cnpj { get; set; }

        public int id { get; set; }
        public string cd_acao_rdz
        {
            get { return _cd_acao_rdz; }
            set { _cd_acao_rdz = (string.IsNullOrEmpty(value) ? value : value.ToUpper()); }
        }
        public string nm_empresa
        {
            get { return _nm_empresa; }
            set { _nm_empresa = (string.IsNullOrEmpty(value) ? value : value.ToUpper()); }
        }
        public string setor_economico
        {
            get { return _setor_economico; }
            set { _setor_economico = (string.IsNullOrEmpty(value) ? value : value.ToUpper()); }
        }
        public string subsetor
        {
            get { return _subsetor; }
            set { _subsetor = (string.IsNullOrEmpty(value) ? value : value.ToUpper()); }
        }
        public string segmento
        {
            get { return _segmento; }
            set { _segmento = (string.IsNullOrEmpty(value) ? value : value.ToUpper()); }
        }
        public string segmento_b3
        {
            get { return _segmento_b3; }
            set { _segmento_b3 = (string.IsNullOrEmpty(value) ? value : value.ToUpper()); }
        }
        public string nm_segmento_b3
        {
            get { return _nm_segmento_b3; }
            set { _nm_segmento_b3 = (string.IsNullOrEmpty(value) ? value : value.ToUpper()); }
        }
        public string cd_acao
        {
            get { return _cd_acao; }
            set { _cd_acao = (string.IsNullOrEmpty(value) ? value : value.ToUpper()); }
        }
        public string tx_cnpj
        {
            get { return _tx_cnpj; }
            set { _tx_cnpj = (string.IsNullOrEmpty(value) ? value : value.ToUpper()); }
        }
        public string vl_cnpj
        {
            get { return _vl_cnpj; }
            set { _vl_cnpj = (string.IsNullOrEmpty(value) ? value : value.ToUpper()); }
        }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }
}
