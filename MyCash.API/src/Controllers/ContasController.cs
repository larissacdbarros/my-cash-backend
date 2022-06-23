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
    public class ContasController : ControllerBase
    {
        private readonly DataContext _context;

        public ContasController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Conta>>> GetAll()
        {
            return await _context.Contas.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Conta>> GetById(int id)
        {
            Conta result = await _context.Contas.FindAsync(id);

            if (result == null)
            {
                return NotFound();
            }
            return result;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Conta body)
        {
            Conta result = await _context.Contas.FindAsync(id);
            if(result == null){
                return NotFound();
            }

            body.ContaId = id ;

            _context.Entry<Conta>(result).State = EntityState.Detached;
            _context.Entry<Conta>(body).State = EntityState.Modified;
            
            _context.Contas.Update(body);
            await _context.SaveChangesAsync();

            return Ok(body);
        }

        [HttpPost]
        public async Task<ActionResult<Conta>> Create(Conta body)
        {
            await _context.Contas.AddAsync(body);
            await _context.SaveChangesAsync();

            return Ok();
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