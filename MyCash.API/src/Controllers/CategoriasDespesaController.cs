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
        public CategoriasDespesaController(DataContext context){
         _context = context;
        }

        [HttpGet]
         public async Task<ActionResult<IEnumerable<CategoriaDespesa>>> GetAll(){
            return await _context.CategoriasDespesas.ToListAsync();
        }
        
        [HttpGet("{Id}")]
        public async Task<ActionResult<CategoriaDespesa>> GetById(int Id){
            CategoriaDespesa result = await _context.CategoriasDespesas.FindAsync(Id);
            if(result==null){
                return NotFound();
            }
            return result;
            
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<CategoriaDespesa>>> Post(CategoriaDespesa result){
            await _context.CategoriasDespesas.AddAsync(result);
            await _context.SaveChangesAsync();
             return Ok(result);
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult> Update(CategoriaDespesa result, int Id){
            CategoriaDespesa resultFind = await _context.CategoriasDespesas.FindAsync(Id);
            if (resultFind == null ){
                return NotFound (); //status 404
            }
            result.CategoriaDespesaId = Id;
            _context.CategoriasDespesas.Update(result);
            await _context.SaveChangesAsync();

            return Ok(result);
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> Delete(int Id){
            CategoriaDespesa resultFind = await _context.CategoriasDespesas.FindAsync(Id);
            if (resultFind == null ){
                return NotFound (); //status 404
            }
            _context.Remove(resultFind);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}