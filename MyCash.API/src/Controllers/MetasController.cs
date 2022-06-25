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
    public class MetasController: ControllerBase
    {
        private readonly DataContext _context;
        public MetasController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Meta>>> GetAll()
        {
            return await _context.Metas.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Meta>> GetById(int id)
        {
            Meta result = await _context.Metas.FindAsync(id);

            if (result == null)
            {
                return NotFound();
            }
            return result;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Meta body)
        {
            Meta result = await _context.Metas.FindAsync(id);
            if(result == null){
                return NotFound();
            }

            body.MetaId = id ;

            _context.Entry<Meta>(result).State = EntityState.Detached;
            _context.Entry<Meta>(body).State = EntityState.Modified;
            
            _context.Metas.Update(body);
            await _context.SaveChangesAsync();

            return Ok(body);
        }

        [HttpPost]
        public async Task<ActionResult<Meta>> Create(Meta body)
        {
            await _context.Metas.AddAsync(body);
            await _context.SaveChangesAsync();

            return Ok(body);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Meta result = await _context.Metas.FindAsync(id);
            if (result == null ){
                return NotFound (); 
            }
            _context.Remove(result);
            await _context.SaveChangesAsync();

            return Ok();

        }
    }
}