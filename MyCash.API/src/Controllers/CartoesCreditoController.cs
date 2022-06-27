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
    public class CartoesCreditoController : ControllerBase
    {
        private readonly DataContext _context;
        
        public CartoesCreditoController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CartaoCredito>>> GetAll()
        {
            List<CartaoCredito> cartaoCredito = await _context.CartoesCredito.ToListAsync();
            
            foreach(CartaoCredito cartao in cartaoCredito){
                BandeiraCartao bandeiraCartao = await _context.BandeirasCartoes.FindAsync(cartao.BandeiraCartaoId);
                Conta conta = await _context.Contas.FindAsync(cartao.ContaId);
                Banco banco = await _context.Bancos.FindAsync(cartao.Conta.BancoId);
                CategoriaConta categoriaConta = await _context.CategoriasContas.FindAsync(cartao.Conta.CategoriaContaId); 
                Usuario usuario = await _context.Usuarios.FindAsync(cartao.Conta.UsuarioId);
                cartao.Conta.CartoesCredito =null;
                cartao.Conta.Usuario = null;

            }
            
            return await _context.CartoesCredito.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CartaoCredito>> GetById(int id)
        {
            CartaoCredito result = await _context.CartoesCredito.FindAsync(id);

            if (result == null)
            {
                return NotFound();
            }
            
            BandeiraCartao bandeiraCartao = await _context.BandeirasCartoes.FindAsync(result.BandeiraCartaoId);
            Conta conta = await _context.Contas.FindAsync(result.ContaId);
            Banco banco = await _context.Bancos.FindAsync(result.Conta.BancoId);
            CategoriaConta categoriaConta = await _context.CategoriasContas.FindAsync(result.Conta.CategoriaContaId); 
            Usuario usuario = await _context.Usuarios.FindAsync(result.Conta.UsuarioId);
            
            

            result.Conta.CartoesCredito =null;
            result.Conta.Usuario = null;
            return result;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CartaoCredito body)
        {
            CartaoCredito result = await _context.CartoesCredito.FindAsync(id);
            if(result == null){
                return NotFound();
            }

            body.CartaoCreditoId = id ;

            BandeiraCartao bandeiraCartao = await _context.BandeirasCartoes.FindAsync(result.BandeiraCartaoId);
            body.BandeiraCartao = bandeiraCartao;
            
            Conta conta = await _context.Contas.FindAsync(result.ContaId);
            body.Conta = conta;

            Banco banco = await _context.Bancos.FindAsync(result.Conta.BancoId);
            body.Conta.Banco = banco;

            CategoriaConta categoriaConta = await _context.CategoriasContas.FindAsync(result.Conta.CategoriaContaId); 
            body.Conta.Categoria = categoriaConta;

            Usuario usuario = await _context.Usuarios.FindAsync(result.Conta.UsuarioId);
            body.Conta.Usuario = usuario;  

            _context.Entry<CartaoCredito>(result).State = EntityState.Detached;
            _context.Entry<CartaoCredito>(body).State = EntityState.Modified;
            
            _context.CartoesCredito.Update(body);
            await _context.SaveChangesAsync();

            body.Conta.CartoesCredito =null;
            body.Conta.Usuario = null;

            return Ok(body);
        }

        [HttpPost]
        public async Task<ActionResult<CartaoCredito>> Create(CartaoCredito body)
        {
            BandeiraCartao bandeiraCartao = await _context.BandeirasCartoes.FindAsync(body.BandeiraCartaoId);
            body.BandeiraCartao = bandeiraCartao;
            
            Conta conta = await _context.Contas.FindAsync(body.ContaId);
            body.Conta = conta;

            Banco banco = await _context.Bancos.FindAsync(body.Conta.BancoId);
            body.Conta.Banco = banco;

            CategoriaConta categoriaConta = await _context.CategoriasContas.FindAsync(body.Conta.CategoriaContaId); 
            body.Conta.Categoria = categoriaConta;

            Usuario usuario = await _context.Usuarios.FindAsync(body.Conta.UsuarioId);
            body.Conta.Usuario = usuario;  

            await _context.CartoesCredito.AddAsync(body);
            await _context.SaveChangesAsync();
            
            body.Conta.CartoesCredito =null;
            body.Conta.Usuario = null;

            return Ok(body);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            CartaoCredito result = await _context.CartoesCredito.FindAsync(id);
            if (result == null ){
                return NotFound (); 
            }
            _context.Remove(result);
            await _context.SaveChangesAsync();

            return Ok();

        }
    }
}