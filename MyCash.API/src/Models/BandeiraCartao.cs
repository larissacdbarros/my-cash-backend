using System.ComponentModel.DataAnnotations;

namespace src.Models
{
    public class BandeiraCartao
    {
       
        public int BandeiraCartaoId { get; set; }
        public string  Nome { get; set; }
        public string Icone { get; set; }
    }
}