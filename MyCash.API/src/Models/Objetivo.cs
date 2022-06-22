using System;

namespace src.Models
{
    public class Objetivo
    {
        public int ObjetivoId { get; set; }
        public string Descricao { get; set; }
        public double ValorMensal { get; set; }
        public double ValorFinal { get; set; }
        public double ValorAtual { get; set; }
        public double PercentualAcumulado { get; set; } 
        public DateTime DataFinal { get; set; }

    }
}