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
    public class DespesasContaController : ControllerBase
    {
        private readonly DataContext _context;
       public DespesasContaController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DespesaConta>>> GetAll()
        {
            return await _context.DespesasConta.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DespesaConta>> GetById(int id)
        {
            DespesaConta result = await _context.DespesasConta.FindAsync(id);

            if (result == null)
            {
                return NotFound();
            }
            return result;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, DespesaConta body)
        {
            DespesaConta result = await _context.DespesasConta.FindAsync(id);
            if(result == null){
                return NotFound();
            }

            body.DespesaContaId = id ;

            _context.Entry<DespesaConta>(result).State = EntityState.Detached;
            _context.Entry<DespesaConta>(body).State = EntityState.Modified;
            
            _context.DespesasConta.Update(body);
            await _context.SaveChangesAsync();

            return Ok(body);
        }

        [HttpPost]
        public async Task<ActionResult<DespesaConta>> Create(DespesaConta body)
        {
            await _context.DespesasConta.AddAsync(body);
            await _context.SaveChangesAsync();

            return Ok(body);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            DespesaConta result = await _context.DespesasConta.FindAsync(id);
            if (result == null ){
                return NotFound (); 
            }
            _context.Remove(result);
            await _context.SaveChangesAsync();

            return Ok();

        }
    }
}