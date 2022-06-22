using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace src.Models
{
    public class CartaoCredito
    {
        
        public int CartaoCreditoId { get; set; }
        public string Descricao { get; set; }
        public int BandeiraCartaoId { get; set; }   
        public BandeiraCartao BandeiraCartao { get; set; }
        public double LimiteCartao { get; set; }
        

    }
}