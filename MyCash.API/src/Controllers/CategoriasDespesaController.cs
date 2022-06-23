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
    public class CategoriasDespesaController : ControllerBase
    {
        private readonly DataContext _context;
        public CategoriasDespesaController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaDespesa>>> GetAll()
        {
            return await _context.CategoriasDespesas.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoriaDespesa>> GetById(int id)
        {
            CategoriaDespesa result = await _context.CategoriasDespesas.FindAsync(id);

            if (result == null)
            {
                return NotFound();
            }
            return result;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CategoriaDespesa body)
        {
            CategoriaDespesa result = await _context.CategoriasDespesas.FindAsync(id);
            if(result == null){
                return NotFound();
            }

            body.CategoriaDespesaId = id ;

            _context.Entry<CategoriaDespesa>(result).State = EntityState.Detached;
            _context.Entry<CategoriaDespesa>(body).State = EntityState.Modified;
            
            _context.CategoriasDespesas.Update(body);
            await _context.SaveChangesAsync();

            return Ok(body);
        }

        [HttpPost]
        public async Task<ActionResult<CategoriaDespesa>> Create(CategoriaDespesa body)
        {
            await _context.CategoriasDespesas.AddAsync(body);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            CategoriaDespesa result = await _context.CategoriasDespesas.FindAsync(id);
            if (result == null ){
                return NotFound (); 
            }
            _context.Remove(result);
            await _context.SaveChangesAsync();

            return Ok();

        }
    }
}