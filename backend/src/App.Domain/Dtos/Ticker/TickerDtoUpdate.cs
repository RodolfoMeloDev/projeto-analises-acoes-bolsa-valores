using System.ComponentModel.DataAnnotations;

namespace App.Domain.Dtos.Ticker
{
    public class TickerDtoUpdate
    {
        [Required(ErrorMessage = "O campo é obrigatório")]
        [StringLength(10, ErrorMessage = "O campo deve ter no máximo {1} caracteres.")]
        public string Ticker { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo deve ter no máximo {1} caracteres.")]
        public string Nome { get; set; }

        [StringLength(100, ErrorMessage = "O campo deve ter no máximo {1} caracteres.")]
        public string Empresa { get; set; }

        [StringLength(18, ErrorMessage = "O campo deve ter no máximo {1} caracteres.")]
        public string CNPJ { get; set; }

        [StringLength(2000, ErrorMessage = "O campo deve ter no máximo {1} caracteres.")]
        public string Descricao { get; set; }

        [StringLength(200, ErrorMessage = "O campo deve ter no máximo {1} caracteres.")]
        public string Site { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        public bool RecuperacaoJudicial { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        public int SegmentoId { get; set; }

        public bool Ativo { get; set; }
    }
}