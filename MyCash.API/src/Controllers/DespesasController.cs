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
         public DespesasController(DataContext context){
         _context = context;
        }

        [HttpGet]
         public async Task<ActionResult<IEnumerable<Despesa>>> GetAll(){
            return await _context.Despesas.ToListAsync();
        }
        
        [HttpGet("{Id}")]
        public async Task<ActionResult<Despesa>> GetById(int Id){
            Despesa result = await _context.Despesas.FindAsync(Id);
            if(result==null){
                return NotFound();
            }
            return result;
            
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<Despesa>>> Post(Despesa result){
            await _context.Despesas.AddAsync(result);
            await _context.SaveChangesAsync();
             return Ok(result);
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult> Update(Despesa result, int Id){
            Despesa resultFind = await _context.Despesas.FindAsync(Id);
            if (resultFind == null ){
                return NotFound (); //status 404
            }
            result.DespesaId = Id;
            _context.Despesas.Update(result);
            await _context.SaveChangesAsync();

            return Ok(result);
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> Delete(int Id){
            Despesa resultFind = await _context.Despesas.FindAsync(Id);
            if (resultFind == null ){
                return NotFound (); //status 404
            }
            _context.Remove(resultFind);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}