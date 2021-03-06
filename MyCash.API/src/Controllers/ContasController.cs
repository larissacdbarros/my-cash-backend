using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyCash.API.Models;
using src.Data;
using src.Models;

namespace src.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContasController : ControllerBase
    {
        private readonly DataContext _context;

        public ContasController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("{usuarioId}")]
        public async Task<ActionResult<IEnumerable<Conta>>> GetByUsuarioId(int usuarioId)
        {
            return await _context.Contas
            .Where(conta => conta.UsuarioId == usuarioId)
            .ToListAsync();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Conta body)
        {
            Conta result = await _context.Contas.FindAsync(id);
            if(result == null){
                return NotFound();
            }

            body.ContaId = id ;


            Banco banco = await _context.Bancos.FindAsync(result.BancoId);
            body.Banco = banco;
            
            CategoriaConta categoriaConta = await _context.CategoriasContas.FindAsync(result.CategoriaContaId);
            body.Categoria = categoriaConta;

            Usuario usuario = await _context.Usuarios.FindAsync(result.UsuarioId);
            body.Usuario = usuario;

            _context.Entry<Conta>(result).State = EntityState.Detached;
            _context.Entry<Conta>(body).State = EntityState.Modified;
            
            _context.Contas.Update(body);
            await _context.SaveChangesAsync();

            body.Usuario.Conta = null;

            return Ok(body);
        }

        [HttpPost]
        public async Task<ActionResult<Conta>> Create(Conta body)
        {
            Banco banco = await _context.Bancos.FindAsync(body.BancoId);
            body.Banco = banco;
            
            CategoriaConta categoriaConta = await _context.CategoriasContas.FindAsync(body.CategoriaContaId);
            body.Categoria = categoriaConta;
            
            Usuario usuario = await _context.Usuarios.FindAsync(body.UsuarioId);
            body.Usuario = usuario; 
            
            await _context.Contas.AddAsync(body);
            await _context.SaveChangesAsync();

            body.Usuario.Conta = null; //as outras contas do usuario s??o colocadas como null
            // apenas na hora de retornar o body para n??o gerar loop j?? que dentro o Usuario tamb??m tem contas 
            
            return Ok(body);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Conta result = await _context.Contas.FindAsync(id);
            if (result == null ){
                return NotFound (); 
            }
            _context.Remove(result);
            await _context.SaveChangesAsync();

            return Ok();

        }
    }
}