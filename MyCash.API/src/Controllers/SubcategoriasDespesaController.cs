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
    public class SubcategoriasDespesaController : ControllerBase
    {
        private readonly DataContext _context;

        public SubcategoriasDespesaController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubcategoriaDespesa>>> GetAll()
        {
            return await _context.SubcategoriasDespesas.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SubcategoriaDespesa>> GetById(int id)
        {
            SubcategoriaDespesa result = await _context.SubcategoriasDespesas.FindAsync(id);

            if (result == null)
            {
                return NotFound();
            }
            return result;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, SubcategoriaDespesa body)
        {
            SubcategoriaDespesa result = await _context.SubcategoriasDespesas.FindAsync(id);
            if(result == null){
                return NotFound();
            }

            body.SubcategoriaDespesaId = id ;

            _context.Entry<SubcategoriaDespesa>(result).State = EntityState.Detached;
            _context.Entry<SubcategoriaDespesa>(body).State = EntityState.Modified;
            
            _context.SubcategoriasDespesas.Update(body);
            await _context.SaveChangesAsync();

            return Ok(body);
        }

        [HttpPost]
        public async Task<ActionResult<SubcategoriaDespesa>> Create(SubcategoriaDespesa body)
        {
            await _context.SubcategoriasDespesas.AddAsync(body);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            SubcategoriaDespesa result = await _context.SubcategoriasDespesas.FindAsync(id);
            if (result == null ){
                return NotFound (); 
            }
            _context.Remove(result);
            await _context.SaveChangesAsync();

            return Ok();

        }
    }
}