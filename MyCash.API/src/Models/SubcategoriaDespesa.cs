namespace src.Models
{
    public class SubcategoriaDespesa
    {
        public int SubcategoriaDespesaId { get; set; } 
        public string Tipo { get; set; }
        public string Icone { get; set; }
        public int CategoriaDespesaId { get; set; }
        public CategoriaDespesa CategoriaDespesa { get; set; }
        
    }
}