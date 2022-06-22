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
         public SubcategoriasDespesaController(DataContext context){
         _context = context;
        }

        [HttpGet]
         public async Task<ActionResult<IEnumerable<SubcategoriaDespesa>>> GetAll(){
            return await _context.SubcategoriasDespesas.ToListAsync();
        }
        
        [HttpGet("{Id}")]
        public async Task<ActionResult<SubcategoriaDespesa>> GetById(int Id){
            SubcategoriaDespesa result = await _context.SubcategoriasDespesas.FindAsync(Id);
            if(result==null){
                return NotFound();
            }
            return result;
            
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<SubcategoriaDespesa>>> Post(SubcategoriaDespesa result){
            await _context.SubcategoriasDespesas.AddAsync(result);
            await _context.SaveChangesAsync();
             return Ok(result);
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult> Update(SubcategoriaDespesa result, int Id){
            SubcategoriaDespesa resultFind = await _context.SubcategoriasDespesas.FindAsync(Id);
            if (resultFind == null ){
                return NotFound (); //status 404
            }
            result.SubcategoriaDespesaId = Id;
            _context.SubcategoriasDespesas.Update(result);
            await _context.SaveChangesAsync();

            return Ok(result);
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> Delete(int Id){
            SubcategoriaDespesa resultFind = await _context.SubcategoriasDespesas.FindAsync(Id);
            if (resultFind == null ){
                return NotFound (); //status 404
            }
            _context.Remove(resultFind);
            await _context.SaveChangesAsync();
            return Ok();
        }
        
    }
}