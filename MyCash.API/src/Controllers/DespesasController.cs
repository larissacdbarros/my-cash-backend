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
    public class DespesasController : ControllerBase
    {
        private readonly DataContext _context;
       public DespesasController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Despesa>>> GetAll()
        {
            return await _context.Despesas.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Despesa>> GetById(int id)
        {
            Despesa result = await _context.Despesas.FindAsync(id);

            if (result == null)
            {
                return NotFound();
            }
            return result;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Despesa body)
        {
            Despesa result = await _context.Despesas.FindAsync(id);
            if(result == null){
                return NotFound();
            }

            body.DespesaId = id ;

            _context.Entry<Despesa>(result).State = EntityState.Detached;
            _context.Entry<Despesa>(body).State = EntityState.Modified;
            
            _context.Despesas.Update(body);
            await _context.SaveChangesAsync();

            return Ok(body);
        }

        [HttpPost]
        public async Task<ActionResult<Despesa>> Create(Despesa body)
        {
            await _context.Despesas.AddAsync(body);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Despesa result = await _context.Despesas.FindAsync(id);
            if (result == null ){
                return NotFound (); 
            }
            _context.Remove(result);
            await _context.SaveChangesAsync();

            return Ok();

        }
    }
}