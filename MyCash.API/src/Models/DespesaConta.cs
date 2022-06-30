using System;
using System.ComponentModel.DataAnnotations.Schema;
using src.Models.DTO;

namespace src.Models
{
    public class DespesaConta
    {
        
        public int DespesaContaId { get; set; }
        public string Descricao { get; set; }
        public int SubcategoriaDespesaId { get; set; }
        public SubcategoriaDespesa SubcategoriaDespesa { get; set; }
        public int ContaId { get; set; }
        public Conta Conta { get; set; }
        public double Valor { get; set; }
        [Column(TypeName="Date")]
        public DateTime Data { get; set; }
    


    public DespesaConta()
    {

    }

    public DespesaConta(DespesaContaReqDTO dto)
        {
            
            this.Descricao = dto.Descricao;
            this.SubcategoriaDespesaId = dto.SubcategoriaDespesaId;
            this.ContaId = dto.ContaId;
            this.Valor = dto.Valor;
            this.Data = dto.Data;

        }
    
}
}