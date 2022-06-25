namespace src.Models
{
    public class SubcategoriaReceita
    {
        public int SubcategoriaReceitaId { get; set; }  
        public string Tipo { get; set; }
        public string Icone { get; set; }
        public int CategoriaReceitaId { get; set; }
        public CategoriaReceita CategoriaReceita { get; set; }

    }
}