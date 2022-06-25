using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyCash.API.Models;
using src.Data;

namespace src.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriasContaController : ControllerBase
    {
        private readonly DataContext _context;
        
        public CategoriasContaController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaConta>>> GetAll()
        {
            return await _context.CategoriasContas.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoriaConta>> GetById(int id)
        {
            CategoriaConta result = await _context.CategoriasContas.FindAsync(id);

            if (result == null)
            {
                return NotFound();
            }
            return result;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CategoriaConta body)
        {
            CategoriaConta result = await _context.CategoriasContas.FindAsync(id);
            if(result == null){
                return NotFound();
            }

            body.CategoriaContaId = id ;

            _context.Entry<CategoriaConta>(result).State = EntityState.Detached;
            _context.Entry<CategoriaConta>(body).State = EntityState.Modified;
            
            _context.CategoriasContas.Update(body);
            await _context.SaveChangesAsync();

            return Ok(body);
        }

        [HttpPost]
        public async Task<ActionResult<CategoriaConta>> Create(CategoriaConta body)
        {
            await _context.CategoriasContas.AddAsync(body);
            await _context.SaveChangesAsync();

            return Ok(body);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            CategoriaConta result = await _context.CategoriasContas.FindAsync(id);
            if (result == null ){
                return NotFound (); 
            }
            _context.Remove(result);
            await _context.SaveChangesAsync();

            return Ok();

        }
    }
}