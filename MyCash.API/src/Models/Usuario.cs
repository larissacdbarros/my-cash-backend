using System.Collections.Generic;

namespace src.Models
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }   
        public string Email { get; set; }
        public string Senha { get; set; }   
        public Conta Conta { get; set; } 

    }
}