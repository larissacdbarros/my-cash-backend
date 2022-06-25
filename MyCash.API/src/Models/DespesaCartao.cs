using System.ComponentModel.DataAnnotations.Schema;

namespace src.Models
{
    public class DespesaCartao
    {
        public int DespesaCartaoId { get; set; }
        public int CartaoCreditoId { get; set; }
        public CartaoCredito CartaoCredito { get; set; }
        public string Descricao { get; set; }    
        public int SubcategoriaDespesaId { get; set; }
        public SubcategoriaDespesa SubcategoriaDespesa  { get; set; }
        public double Valor { get; set; }
    }
}