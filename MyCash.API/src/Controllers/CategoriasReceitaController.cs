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
       
        public CategoriasReceitaController(DataContext context){
         _context = context;
        }
        [HttpGet]
         public async Task<ActionResult<IEnumerable<CategoriaReceita>>> GetAll(){
            return await _context.CategoriasReceitas.ToListAsync();
        }
        
        [HttpGet("{Id}")]
        public async Task<ActionResult<CategoriaReceita>> GetById(int Id){
            CategoriaReceita result = await _context.CategoriasReceitas.FindAsync(Id);
            if(result==null){
                return NotFound();
            }
            return result;
            
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<CategoriaReceita>>> Post(CategoriaReceita result){
            await _context.CategoriasReceitas.AddAsync(result);
            await _context.SaveChangesAsync();
             return Ok(result);
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult> Update(CategoriaReceita result, int Id){
            CategoriaReceita resultFind = await _context.CategoriasReceitas.FindAsync(Id);
            if (resultFind == null ){
                return NotFound (); //status 404
            }
            result.CategoriaReceitaId = Id;
            _context.CategoriasReceitas.Update(result);
            await _context.SaveChangesAsync();

            return Ok(result);
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> Delete(int Id){
            CategoriaReceita resultFind = await _context.CategoriasReceitas.FindAsync(Id);
            if (resultFind == null ){
                return NotFound (); //status 404
            }
            _context.Remove(resultFind);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}