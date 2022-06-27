using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace src.Models
{
    public class DespesaConta
    {
        public int DespesaContaId { get; set; }
        public string Descricao { get; set; }    
        public int SubcategoriaDespesaId { get; set; }
        public SubcategoriaDespesa SubcategoriaDespesa  { get; set; }
        public int ContaId { get; set; }
        public Conta Conta { get; set; }
        public double Valor { get; set; }
        public DateTime Data { get; set; }
    }
}