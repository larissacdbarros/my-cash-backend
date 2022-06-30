using System;
using System.ComponentModel.DataAnnotations.Schema;
using src.Models.DTO;

namespace src.Models
{
    public class DespesaCartao
    {
        
        public int DespesaCartaoId { get; set; }
        public int CartaoCreditoId { get; set; }
        public CartaoCredito CartaoCredito { get; set; }
        public string Descricao { get; set; }
        public int SubcategoriaDespesaId { get; set; }
        public SubcategoriaDespesa SubcategoriaDespesa { get; set; }
        public double Valor { get; set; }
        [Column(TypeName="Date")]
        public DateTime Data { get; set; }
        public int FaturaId { get; set; }
        public Fatura Fatura { get; set; }

        public DespesaCartao(){}

        public DespesaCartao(DespesaCartaoReqDTO dto)
        {
            this.CartaoCreditoId = dto.CartaoCreditoId; 
            this.Descricao = dto.Descricao;
            this.SubcategoriaDespesaId = dto.SubcategoriaDespesaId;
            this.Valor = dto.Valor;
            this.Data = dto.Data;
            this.FaturaId = dto.FaturaId;   

        }
        


    }
}