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
    public class BandeirasCartaoController : ControllerBase
    {
        private readonly DataContext _context;
        
        public BandeirasCartaoController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BandeiraCartao>>> GetAll()
        {
            return await _context.BandeirasCartoes.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BandeiraCartao>> GetById(int id)
        {
            BandeiraCartao result = await _context.BandeirasCartoes.FindAsync(id);

            if (result == null)
            {
                return NotFound();
            }
            return result;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, BandeiraCartao body)
        {
            BandeiraCartao result = await _context.BandeirasCartoes.FindAsync(id);
            if(result == null){
                return NotFound();
            }

            body.BandeiraCartaoId = id ;

            _context.Entry<BandeiraCartao>(result).State = EntityState.Detached;
            _context.Entry<BandeiraCartao>(body).State = EntityState.Modified;
            
            _context.BandeirasCartoes.Update(body);
            await _context.SaveChangesAsync();

            return Ok(body);
        }

        [HttpPost]
        public async Task<ActionResult<BandeiraCartao>> Create(BandeiraCartao body)
        {
            await _context.BandeirasCartoes.AddAsync(body);
            await _context.SaveChangesAsync();

            return Ok(body);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            BandeiraCartao result = await _context.BandeirasCartoes.FindAsync(id);
            if (result == null ){
                return NotFound (); 
            }
            _context.Remove(result);
            await _context.SaveChangesAsync();

            return Ok();

        }
    }
}