using System.ComponentModel.DataAnnotations;
using App.Domain.Enums;

namespace App.Domain.Dtos.Ticker
{
    public class TickerDtoCreate
    {

        [Required(ErrorMessage = "O campo é obrigatório")]
        [StringLength(10, ErrorMessage = "O campo deve ter no máximo {1} caracteres.")]
        public string Ticker { get; set; }

        [Required]
        public int BaseTickerId { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        public TypeTicker TypeTicker { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        public bool JudicialRecovery { get; set; }
    }
}
