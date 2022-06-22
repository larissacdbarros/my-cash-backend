using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyCash.API.Models;
using src.Data;

namespace src.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriasContaController : ControllerBase
    {
        private readonly DataContext _context;
         public CategoriasContaController(DataContext context){
         _context = context;
        }
        
        [HttpGet]
         public async Task<ActionResult<IEnumerable<CategoriaConta>>> GetAll(){
            return await _context.CategoriasContas.ToListAsync();
        }
        
        [HttpGet("{Id}")]
        public async Task<ActionResult<CategoriaConta>> GetById(int Id){
            CategoriaConta result = await _context.CategoriasContas.FindAsync(Id);
            if(result==null){
                return NotFound();
            }
            return result;
            
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<CategoriaConta>>> Post(CategoriaConta result){
            await _context.CategoriasContas.AddAsync(result);
            await _context.SaveChangesAsync();
             return Ok(result);
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult> Update(CategoriaConta result, int Id){
            CategoriaConta resultFind = await _context.CategoriasContas.FindAsync(Id);
            if (resultFind == null ){
                return NotFound (); //status 404
            }
            result.CategoriaContaId = Id;
            _context.CategoriasContas.Update(result);
            await _context.SaveChangesAsync();

            return Ok(result);
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> Delete(int Id){
            CategoriaConta resultFind = await _context.CategoriasContas.FindAsync(Id);
            if (resultFind == null ){
                return NotFound (); //status 404
            }
            _context.Remove(resultFind);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}