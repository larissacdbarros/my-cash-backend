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
    public class ReceitasController : ControllerBase
    {
        private readonly DataContext _context;
        public ReceitasController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Receita>>> GetAll()
        {
            return await _context.Receitas.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Receita>> GetById(int id)
        {
            Receita result = await _context.Receitas.FindAsync(id);

            if (result == null)
            {
                return NotFound();
            }
            return result;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Receita body)
        {
            Receita result = await _context.Receitas.FindAsync(id);
            if(result == null){
                return NotFound();
            }

            body.ReceitaId = id ;

            _context.Entry<Receita>(result).State = EntityState.Detached;
            _context.Entry<Receita>(body).State = EntityState.Modified;
            
            _context.Receitas.Update(body);
            await _context.SaveChangesAsync();

            return Ok(body);
        }

        [HttpPost]
        public async Task<ActionResult<Receita>> Create(Receita body)
        {
            await _context.Receitas.AddAsync(body);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Receita result = await _context.Receitas.FindAsync(id);
            if (result == null ){
                return NotFound (); 
            }
            _context.Remove(result);
            await _context.SaveChangesAsync();

            return Ok();

        }
    }
}