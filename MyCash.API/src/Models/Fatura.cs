using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using src.Models.DTO;

namespace src.Models
{
    public class Fatura
    {
        
        public int FaturaId { get; set; }
        [Column(TypeName="Date")]
        public DateTime DataFechamentoFatura { get; set; }
        public int Mes { get; set; }
        public int Ano { get; set; }
        public double ValorFatura { get; set; }
        public bool isFaturaFechada { get; set; } 
        public bool isFaturaPaga { get; set; }
        public IEnumerable<DespesaCartao> DespesasCartao { get; set; }

        public Fatura(FaturaReqDTO dto)
        {
            
            this.DataFechamentoFatura = dto.DataFechamentoFatura;
            this.Mes = dto.Mes;
            this.Ano = dto.Ano;
            this.ValorFatura = dto.ValorFatura;
            this.isFaturaFechada = dto.isFaturaFechada;
            this.isFaturaPaga = dto.isFaturaPaga;

        }
        public Fatura()
        {
            
        }
    }
}