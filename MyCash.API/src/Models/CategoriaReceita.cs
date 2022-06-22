namespace src.Models
{
    public class CategoriaReceita
    {
        public int CategoriaReceitaId { get; set; }
        public string Tipo { get; set; }    
        public string Icone { get; set; }
        public int SubcategoriaReceitaId { get; set; }
        public SubcategoriaReceita SubcatecoriaReceita { get; set; }

    }
}