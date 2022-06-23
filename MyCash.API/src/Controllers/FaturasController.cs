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
    public class FaturasController : ControllerBase
    {
        private readonly DataContext _context;
        public FaturasController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Fatura>>> GetAll()
        {
            return await _context.Faturas.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Fatura>> GetById(int id)
        {
            Fatura result = await _context.Faturas.FindAsync(id);

            if (result == null)
            {
                return NotFound();
            }
            return result;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Fatura body)
        {
            Fatura result = await _context.Faturas.FindAsync(id);
            if(result == null){
                return NotFound();
            }

            body.FaturaId = id ;

            _context.Entry<Fatura>(result).State = EntityState.Detached;
            _context.Entry<Fatura>(body).State = EntityState.Modified;
            
            _context.Faturas.Update(body);
            await _context.SaveChangesAsync();

            return Ok(body);
        }

        [HttpPost]
        public async Task<ActionResult<Fatura>> Create(Fatura body)
        {
            await _context.Faturas.AddAsync(body);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Fatura result = await _context.Faturas.FindAsync(id);
            if (result == null ){
                return NotFound (); 
            }
            _context.Remove(result);
            await _context.SaveChangesAsync();

            return Ok();

        }
    }
}