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
    public class ObjetivosController : ControllerBase
    {
        private readonly DataContext _context;
        public ObjetivosController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Objetivo>>> GetAll()
        {
            return await _context.Objetivos.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Objetivo>> GetById(int id)
        {
            Objetivo result = await _context.Objetivos.FindAsync(id);

            if (result == null)
            {
                return NotFound();
            }
            return result;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Objetivo body)
        {
            Objetivo result = await _context.Objetivos.FindAsync(id);
            if(result == null){
                return NotFound();
            }

            body.ObjetivoId = id ;

            _context.Entry<Objetivo>(result).State = EntityState.Detached;
            _context.Entry<Objetivo>(body).State = EntityState.Modified;
            
            _context.Objetivos.Update(body);
            await _context.SaveChangesAsync();

            return Ok(body);
        }

        [HttpPost]
        public async Task<ActionResult<Objetivo>> Create(Objetivo body)
        {
            await _context.Objetivos.AddAsync(body);
            await _context.SaveChangesAsync();

            return Ok(body);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Objetivo result = await _context.Objetivos.FindAsync(id);
            if (result == null ){
                return NotFound (); 
            }
            _context.Remove(result);
            await _context.SaveChangesAsync();

            return Ok();

        }
    }
}