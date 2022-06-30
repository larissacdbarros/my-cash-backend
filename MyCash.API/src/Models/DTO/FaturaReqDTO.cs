using System;

namespace src.Models.DTO
{
    public class FaturaReqDTO
    {
        public DateTime DataFechamentoFatura { get; set; }
        public DateTime DataVencimentoFatura { get; set; }
        public string Mes { get; set; }
        public double ValorFatura { get; set; } 
        public bool isFaturaVencida { get; set; }
        public bool isFaturaPaga { get; set; }
        
    }
}