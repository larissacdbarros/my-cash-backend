using System;
using System.Collections.Generic;
using src.Models.DTO;

namespace src.Models
{
    public class Fatura
    {
        
        public int FaturaId { get; set; }
        public DateTime DataFechamentoFatura { get; set; }
        public DateTime DataVencimentoFatura { get; set; }
        public string Mes { get; set; }
        public double ValorFatura { get; set; }
        public bool isFaturaVencida { get; set; }
        public bool isFaturaPaga { get; set; }
        public IEnumerable<DespesaCartao> DespesasCartao { get; set; }

        public Fatura(FaturaReqDTO dto)
        {
            
            this.DataFechamentoFatura = dto.DataFechamentoFatura;
            this.DataVencimentoFatura = dto.DataVencimentoFatura;
            this.Mes = dto.Mes;
            this.ValorFatura = dto.ValorFatura;
            this.isFaturaVencida = dto.isFaturaVencida;
            this.isFaturaPaga = dto.isFaturaPaga;

        }
        public Fatura()
        {
            
        }
    }
}