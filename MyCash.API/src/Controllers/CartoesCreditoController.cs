using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            }
            
            return await _context.CartoesCredito.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CartaoCredito>> GetById(int id)
        {
            CartaoCredito result = await _context.CartoesCredito.FindAsync(id);
            BandeiraCartao bandeiraCartao = await _context.BandeirasCartoes.FindAsync(result.BandeiraCartaoId);

            if (result == null)
            {
                return NotFound();
            }
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
            BandeiraCartao bandeiraCartao = await _context.BandeirasCartoes.FindAsync(body.BandeiraCartaoId);
            body.BandeiraCartao = bandeiraCartao;

            _context.Entry<CartaoCredito>(result).State = EntityState.Detached;
            _context.Entry<CartaoCredito>(body).State = EntityState.Modified;
            
            _context.CartoesCredito.Update(body);
            await _context.SaveChangesAsync();

            return Ok(body);
        }

        [HttpPost]
        public async Task<ActionResult<CartaoCredito>> Create(CartaoCredito body)
        {
            BandeiraCartao bandeiraCartao = await _context.BandeirasCartoes.FindAsync(body.BandeiraCartaoId);
            body.BandeiraCartao = bandeiraCartao;
            
            Conta conta = await _context.Contas.FindAsync(body.ContaId);
            body.Conta = conta;

            await _context.CartoesCredito.AddAsync(body);
            await _context.SaveChangesAsync();
            
            body.Conta.CartoesCredito =null;

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