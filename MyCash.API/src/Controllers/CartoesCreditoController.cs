using System.Collections.Generic;
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
        
         public CartoesCreditoController(DataContext context){
         _context = context;
        }

         [HttpGet]
         public async Task<ActionResult<IEnumerable<CartaoCredito>>> GetAll(){
            return await _context.CartoesCredito.ToListAsync();
        }
        
        [HttpGet("{Id}")]
        public async Task<ActionResult<CartaoCredito>> GetById(int Id){
            CartaoCredito result = await _context.CartoesCredito.FindAsync(Id);
            if(result==null){
                return NotFound();
            }
            return result;
            
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<CartaoCredito>>> Post(CartaoCredito result){
            await _context.CartoesCredito.AddAsync(result);
            await _context.SaveChangesAsync();
             return Ok(result);
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult> Update(CartaoCredito result, int Id){
            CartaoCredito resultFind = await _context.CartoesCredito.FindAsync(Id);
            if (resultFind == null ){
                return NotFound (); //status 404
            }
            result.CartaoCreditoId = Id;
            _context.CartoesCredito.Update(result);
            await _context.SaveChangesAsync();

            return Ok(result);
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> Delete(int Id){
            CartaoCredito banco = await _context.CartoesCredito.FindAsync(Id);
            if (banco == null ){
                return NotFound (); //status 404
            }
            _context.Remove(banco);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}