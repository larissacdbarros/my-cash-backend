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
         public ReceitasController(DataContext context){
         _context = context;
        }

        [HttpGet]
         public async Task<ActionResult<IEnumerable<Receita>>> GetAll(){
            return await _context.Receitas.ToListAsync();
        }
        
        [HttpGet("{Id}")]
        public async Task<ActionResult<Receita>> GetById(int Id){
            Receita result = await _context.Receitas.FindAsync(Id);
            if(result==null){
                return NotFound();
            }
            return result;
            
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<Receita>>> Post(Receita result){
            await _context.Receitas.AddAsync(result);
            await _context.SaveChangesAsync();
             return Ok(result);
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult> Update(Receita result, int Id){
            Receita resultFind = await _context.Receitas.FindAsync(Id);
            if (resultFind == null ){
                return NotFound (); //status 404
            }
            result.ReceitaId = Id;
            _context.Receitas.Update(result);
            await _context.SaveChangesAsync();

            return Ok(result);
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> Delete(int Id){
            Receita resultFind = await _context.Receitas.FindAsync(Id);
            if (resultFind == null ){
                return NotFound (); //status 404
            }
            _context.Remove(resultFind);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}