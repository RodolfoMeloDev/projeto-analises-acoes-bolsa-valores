using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Domain.Dtos.Formula
{
    public class OptionsFormula
    {
        public bool IncluirItensRecuperacaoJudicial { get; set; }        
        public decimal? LiquidezMinima { get; set; }
        public decimal? EvEbitMinima { get; set; }
        public decimal? EvEbitMaxima { get; set; }
        public decimal? PLMinima { get; set; }
        public decimal? PLMaxima { get; set; }
        public decimal? MargemEbitMinima { get; set; }
        public decimal? MargemEbitMaxima { get; set; }
        
    }
}