namespace src.Models
{
    public class Meta
    {
        public int MetaId { get; set; }
        public int CategoriaDespesaId { get; set; }
        public CategoriaDespesa CategoriaDespesa  { get; set; }
        public double LimiteParaGastar { get; set; }
        public double PercentualGasto { get; set; }
    }
}