using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace src.Models.DTO
{
    public class ReceitaReqDTO
    {
        public string Descricao { get; set; }
        public int SubcategoriaReceitaId { get; set; } 
        public int ContaId { get; set; }
        
        [Column(TypeName="Date")]
        public DateTime Data { get; set; }
        public double Valor { get; set; }
    }
}