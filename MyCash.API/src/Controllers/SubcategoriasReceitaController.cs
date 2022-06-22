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

        public SubcategoriasReceitaController(DataContext context){
         _context = context;
        }

        [HttpGet]
         public async Task<ActionResult<IEnumerable<SubcategoriaReceita>>> GetAll(){
            return await _context.SubcategoriasReceitas.ToListAsync();
        }
        
        [HttpGet("{Id}")]
        public async Task<ActionResult<SubcategoriaReceita>> GetById(int Id){
            SubcategoriaReceita result = await _context.SubcategoriasReceitas.FindAsync(Id);
            if(result==null){
                return NotFound();
            }
            return result;
            
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<SubcategoriaReceita>>> Post(SubcategoriaReceita result){
            await _context.SubcategoriasReceitas.AddAsync(result);
            await _context.SaveChangesAsync();
             return Ok(result);
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult> Update(SubcategoriaReceita result, int Id){
            SubcategoriaReceita resultFind = await _context.SubcategoriasReceitas.FindAsync(Id);
            if (resultFind == null ){
                return NotFound (); //status 404
            }
            result.SubcategoriaReceitaId = Id;
            _context.SubcategoriasReceitas.Update(result);
            await _context.SaveChangesAsync();

            return Ok(result);
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> Delete(int Id){
            SubcategoriaReceita resultFind = await _context.SubcategoriasReceitas.FindAsync(Id);
            if (resultFind == null ){
                return NotFound (); //status 404
            }
            _context.Remove(resultFind);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}