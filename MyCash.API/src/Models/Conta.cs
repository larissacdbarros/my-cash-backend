using MyCash.API.Models;

namespace src.Models
{
    public class Conta
    {
        public int ContaId { get; set; }
        public string Descricao { get; set; }   
        public int BancoId { get; set; }
        public Banco Banco { get; set; }
        public double SaldoAtual { get; set; }
        public int CategoriaContaId { get; set; }
        public CategoriaConta Categoria { get; set; }
        public int UsuarioId { get; set; } 
        public Usuario Usuario { get; set; }    

    }
}