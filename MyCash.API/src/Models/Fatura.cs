using System;
using System.Collections.Generic;

namespace src.Models
{
    public class Fatura
    {
        public int FaturaId { get; set; }
        public int CartaoCreditoId { get; set; }
        public CartaoCredito CartaoCredito { get; set; }
        public DateTime DataFechamentoFatura { get; set; }
        public DateTime DataVencimentoFatura { get; set; }
        public string Mes { get; set; }
        public double ValorFatura { get; set; } 
        public bool isFaturaVencida { get; set; }
        public bool isFaturaPaga { get; set; }
        
    }
}