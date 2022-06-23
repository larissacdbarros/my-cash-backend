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
    public class SubcategoriasReceitaController : ControllerBase
    {
        private readonly DataContext _context;

        public SubcategoriasReceitaController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubcategoriaReceita>>> GetAll()
        {
            return await _context.SubcategoriasReceitas.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SubcategoriaReceita>> GetById(int id)
        {
            SubcategoriaReceita result = await _context.SubcategoriasReceitas.FindAsync(id);

            if (result == null)
            {
                return NotFound();
            }
            return result;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, SubcategoriaReceita body)
        {
            SubcategoriaReceita result = await _context.SubcategoriasReceitas.FindAsync(id);
            if(result == null){
                return NotFound();
            }

            body.SubcategoriaReceitaId = id ;

            _context.Entry<SubcategoriaReceita>(result).State = EntityState.Detached;
            _context.Entry<SubcategoriaReceita>(body).State = EntityState.Modified;
            
            _context.SubcategoriasReceitas.Update(body);
            await _context.SaveChangesAsync();

            return Ok(body);
        }

        [HttpPost]
        public async Task<ActionResult<SubcategoriaReceita>> Create(SubcategoriaReceita body)
        {
            await _context.SubcategoriasReceitas.AddAsync(body);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            SubcategoriaReceita result = await _context.SubcategoriasReceitas.FindAsync(id);
            if (result == null ){
                return NotFound (); 
            }
            _context.Remove(result);
            await _context.SaveChangesAsync();

            return Ok();

        }
    }

    
}