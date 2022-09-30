using System.ComponentModel.DataAnnotations;
using App.Domain.Enums;

namespace App.Domain.Dtos.Ticker
{
    public class TickerDtoCreate
    {
        [StringLength(10, ErrorMessage = "O campo deve ter no máximo {1} caracteres.")]
        public string BaseTicker { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        [StringLength(10, ErrorMessage = "O campo deve ter no máximo {1} caracteres.")]
        public string Ticker { get; set; }

        [StringLength(100, ErrorMessage = "O campo deve ter no máximo {1} caracteres.")]
        public string Company { get; set; }

        [StringLength(18, ErrorMessage = "O campo deve ter no máximo {1} caracteres.")]
        public string CNPJ { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        public TypeTicker TypeTicker { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        public bool JudicialRecovery { get; set; }

        public int? SegmentId { get; set; }
    }
}
