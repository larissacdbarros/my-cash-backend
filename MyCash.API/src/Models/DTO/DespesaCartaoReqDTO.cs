using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace src.Models.DTO
{
    public class DespesaCartaoReqDTO
    {
        public string Descricao { get; set; }
        public int SubcategoriaDespesaId { get; set; }
        public double Valor { get; set; }
        
        [Column(TypeName="Date")]
        public DateTime Data { get; set; }
        public int ContaId { get; set; }
    }
}