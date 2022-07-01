using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyCash.API.Models;
using src.Data;
using src.Models;

namespace MyCash.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly DataContext _context;

        public UsuariosController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetAll()
        {
            return await _context.Usuarios.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetById(int id)
        {
            Usuario result = await _context.Usuarios.FindAsync(id);

            if (result == null)
            {
                return NotFound();
            }
            return result;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Usuario body)
        {
            Usuario result = await _context.Usuarios.FindAsync(id);
            if(result == null){
                return NotFound();
            }

            body.UsuarioId = id ;
            

            _context.Entry<Usuario>(result).State = EntityState.Detached;
            _context.Entry<Usuario>(body).State = EntityState.Modified;
            
            _context.Usuarios.Update(body);
            await _context.SaveChangesAsync();

            return Ok(body);
        }

        [HttpPost]
        public async Task<ActionResult<Usuario>> Create(Usuario body)
        {
            await _context.Usuarios.AddAsync(body);
            await _context.SaveChangesAsync();

            Banco banco = new Banco ();
            banco.Nome = "Meu Banco";
            banco.Imagem = "";

            await _context.Bancos.AddAsync(banco);
            await _context.SaveChangesAsync();

            Conta conta = new Conta ();
            conta.BancoId = banco.BancoId;
            conta.CategoriaContaId = VerificarCategoriaConta();
            conta.Descricao = "Minha Conta";
            conta.SaldoAtual = 0.0;
            conta.UsuarioId = body.UsuarioId;
            
            await _context.Contas.AddAsync(conta);
            await _context.SaveChangesAsync();

            return Ok(body);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Usuario result = await _context.Usuarios.FindAsync(id);
            if (result == null ){
                return NotFound (); 
            }
            _context.Remove(result);
            await _context.SaveChangesAsync();

            return Ok();

        }

        [HttpGet("email/{email}/senha/{senha}")]
        public async Task<ActionResult<Usuario>> AutenticarUsuario(string email, string senha){
            var usuario = await _context.Usuarios
            .Include(usuario => usuario.Conta)
            .Where(usuario => usuario.Email == email &&
            usuario.Senha == senha ).FirstOrDefaultAsync();

            if(usuario == null){
                return NotFound();
            }

            return usuario;

        }

        private int VerificarCategoriaConta(){

            var categoria = _context.CategoriasContas
                        .Where (conta => conta.Tipo == "Conta Corrente" )
                        .FirstOrDefault();

            if(categoria != null){
                return categoria.CategoriaContaId;

            }else{
                CategoriaConta categoriaConta = new CategoriaConta();
                categoriaConta.Tipo = "Conta Corrente";
                categoriaConta.Icone = "";

                _context.CategoriasContas.Add(categoriaConta);
                _context.SaveChanges();

                return categoriaConta.CategoriaContaId;

        }

         
    }
}}