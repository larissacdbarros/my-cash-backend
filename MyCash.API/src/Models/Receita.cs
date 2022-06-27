using System;
using System.ComponentModel.DataAnnotations;

namespace src.Models
{
    public class Receita
    {
        public int ReceitaId { get; set; }
        public string Descricao { get; set; }
        public int SubcategoriaReceitaId { get; set; } 
        public SubcategoriaReceita SubcategoriaReceita { get; set; }
        public int ContaId { get; set; }
        public Conta Conta { get; set; }
        public DateTime Data { get; set; }

    }
}