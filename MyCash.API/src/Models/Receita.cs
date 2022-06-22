namespace src.Models
{
    public class Receita
    {
        public int ReceitaId { get; set; }
        public string Descricao { get; set; }
        public int CategoriaReceitaId { get; set; } 
        public CategoriaReceita CategoriaReceita { get; set; }
        public int ContaId { get; set; }
        public Conta Conta { get; set; }

    }
}