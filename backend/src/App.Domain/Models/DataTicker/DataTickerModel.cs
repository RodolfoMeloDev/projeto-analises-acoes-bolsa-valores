using System;

namespace App.Domain.Models.DataTicker
{
    public class DataTickerModel
    {
        public int id { get; set; }
        public string cd_acao_rdz { get; set; }
        public string nm_empresa { get; set; }
        public string setor_economico { get; set; }
        public string subsetor { get; set; }
        public string segmento { get; set; }
        public string segmento_b3 { get; set; }
        public string nm_segmento_b3 { get; set; }
        public string cd_acao { get; set; }
        public string tx_cnpj { get; set; }
        public string vl_cnpj { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }
}
