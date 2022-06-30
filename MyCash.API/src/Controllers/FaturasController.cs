using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using src.Data;
using src.Models;
using src.Models.DTO;

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
            Fatura result = await _context.Faturas
            .Where(fatura => fatura.FaturaId == id)
            .FirstOrDefaultAsync();

            if (result == null)
            {
                return NotFound();
            }
            return result;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, FaturaReqDTO body)
        {
            Fatura result = await _context.Faturas.FindAsync(id);
            
            if(result == null){
                return NotFound();
            }

            Fatura fatura = new Fatura(body);
            fatura.FaturaId = id;

            _context.Entry<Fatura>(result).State = EntityState.Detached;
            _context.Entry<Fatura>(fatura).State = EntityState.Modified;
            
            _context.Faturas.Update(fatura);
            await _context.SaveChangesAsync();

            return Ok(fatura);
        }

        [HttpPost]
        public async Task<ActionResult<Fatura>> Create(FaturaReqDTO body)
        {
            Fatura fatura = new Fatura(body);
             
            
            await _context.Faturas.AddAsync(fatura);
            await _context.SaveChangesAsync();

            return Ok(body);
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