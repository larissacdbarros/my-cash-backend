using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace src.Models.DTO
{
    public class FaturaDTO
    {
        public FaturaDTO(int faturaId, int ano, int mes, double valorFatura)
        {
            this.FaturaId = faturaId;
            this.Ano = ano;
            this.Mes = mes;
            this.ValorFatura = valorFatura;

        }
        public int FaturaId { get; set; }
        public int Ano { get; set; }
        public int Mes { get; set; }
        public double ValorFatura { get; set; }
         
          public FaturaDTO()
          {
            
          }

    }
}