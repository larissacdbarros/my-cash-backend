namespace src.Models
{
    public class Despesa
    {
        public int DespesaId { get; set; }
        public bool IsDespesaCartaoCredito { get; set; }
        public int CartaoCreditoId { get; set; }
        public CartaoCredito CartaoCredito { get; set; }
        public string Descricao { get; set; }    
        public int CategoriaDespesaId { get; set; }
        public CategoriaDespesa CategoriaDespesa  { get; set; }
        public int ContaId { get; set; }
        public Conta Conta { get; set; }
    }
}