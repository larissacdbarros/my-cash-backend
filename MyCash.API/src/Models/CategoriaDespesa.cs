namespace src.Models
{
    public class CategoriaDespesa
    {
        public int CategoriaDespesaId { get; set; }
        public string Tipo { get; set; }
        public string Icone { get; set; }
        public int SubcategoriaDespesaId { get; set; }
        public SubcategoriaDespesa SubcategoriaDespesa { get; set; }

    }
}