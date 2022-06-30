using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace src.Models.DTO
{
    public class FaturaReqDTO
    {
        [Column(TypeName="Date")]
        public DateTime DataFechamentoFatura { get; set; }
        public int Ano { get; set; }
        public int Mes { get; set; }
        public double ValorFatura { get; set; } 
        public bool isFaturaFechada { get; set; }
        public bool isFaturaPaga { get; set; }
        public int ContaId { get; set; }
        
    }
}