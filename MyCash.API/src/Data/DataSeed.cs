using src.Models;

namespace src.Data
{
    public class DataSeed
    {
        private readonly DataContext _context;

        public DataSeed(DataContext context)
        {
            _context = context;
        }

        public void Seed(){
            Usuario usuario = new Usuario();
            usuario.Nome = "Larissa";
            this._context.Usuarios.AddAsync(usuario);
            this._context.SaveChangesAsync();
        }
    }
}