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
    public class BancosController : ControllerBase
    {
        private readonly DataContext _context;
                
        public BancosController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Banco>>> GetAll()
        {
            return await _context.Bancos.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Banco>> GetById(int id)
        {
            Banco result = await _context.Bancos.FindAsync(id);

            if (result == null)
            {
                return NotFound();
            }
            return result;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Banco body)
        {
            Banco result = await _context.Bancos.FindAsync(id);
            if(result == null){
                return NotFound();
            }

            body.BancoId = id ;

            _context.Entry<Banco>(result).State = EntityState.Detached;
            _context.Entry<Banco>(body).State = EntityState.Modified;
            
            _context.Bancos.Update(body);
            await _context.SaveChangesAsync();

            return Ok(body);
        }

        [HttpPost]
        public async Task<ActionResult<Banco>> Create(Banco body)
        {
            await _context.Bancos.AddAsync(body);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Banco result = await _context.Bancos.FindAsync(id);
            if (result == null ){
                return NotFound (); 
            }
            _context.Remove(result);
            await _context.SaveChangesAsync();

            return Ok();

        }

    }
}