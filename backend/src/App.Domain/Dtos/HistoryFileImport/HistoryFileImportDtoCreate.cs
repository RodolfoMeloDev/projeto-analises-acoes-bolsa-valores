using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App.Domain.Dtos.HistoryFileImport
{
    public class HistoryFileImportDtoCreate
    {
        [Required(ErrorMessage = "O campo é obrigatório")]
        public int ArquivoImportacaoId { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        public int TickerId { get; set; }
        
        [Required(ErrorMessage = "O campo é obrigatório")]
        public float PrecoUnitario { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        public float PrecoLucro { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        public float Roic { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        public float EvEbit { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        public float MargemEbit { get; set; }
        
        public float DividendYield { get; set; }
        public float PrecoValorPatrimonial { get; set; }
        public float LiquidezMediaDiaria { get; set; }
        public float ValorMercado { get; set; }
        public float VolumeFinanceiro { get; set; }        
    }
}