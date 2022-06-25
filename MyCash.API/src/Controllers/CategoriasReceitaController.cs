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
    public class CategoriasReceitaController : ControllerBase
    {
        private readonly DataContext _context;
        public CategoriasReceitaController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaReceita>>> GetAll()
        {
            return await _context.CategoriasReceitas.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoriaReceita>> GetById(int id)
        {
            CategoriaReceita result = await _context.CategoriasReceitas.FindAsync(id);

            if (result == null)
            {
                return NotFound();
            }
            return result;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CategoriaReceita body)
        {
            CategoriaReceita result = await _context.CategoriasReceitas.FindAsync(id);
            if(result == null){
                return NotFound();
            }

            body.CategoriaReceitaId = id ;

            _context.Entry<CategoriaReceita>(result).State = EntityState.Detached;
            _context.Entry<CategoriaReceita>(body).State = EntityState.Modified;
            
            _context.CategoriasReceitas.Update(body);
            await _context.SaveChangesAsync();

            return Ok(body);
        }

        [HttpPost]
        public async Task<ActionResult<CategoriaReceita>> Create(CategoriaReceita body)
        {
            await _context.CategoriasReceitas.AddAsync(body);
            await _context.SaveChangesAsync();

            return Ok(body);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            CategoriaReceita result = await _context.CategoriasReceitas.FindAsync(id);
            if (result == null ){
                return NotFound (); 
            }
            _context.Remove(result);
            await _context.SaveChangesAsync();

            return Ok();

        }
    }
}