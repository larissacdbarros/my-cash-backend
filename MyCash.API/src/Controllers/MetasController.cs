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
    public class MetasController: ControllerBase
    {
        private readonly DataContext _context;
        public MetasController(DataContext context){
         _context = context;
        }

        [HttpGet]
         public async Task<ActionResult<IEnumerable<Meta>>> GetAll(){
            return await _context.Metas.ToListAsync();
        }
        
        [HttpGet("{Id}")]
        public async Task<ActionResult<Meta>> GetById(int Id){
            Meta result = await _context.Metas.FindAsync(Id);
            if(result==null){
                return NotFound();
            }
            return result;
            
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<Meta>>> Post(Meta result){
            await _context.Metas.AddAsync(result);
            await _context.SaveChangesAsync();
             return Ok(result);
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult> Update(Meta result, int Id){
            Meta resultFind = await _context.Metas.FindAsync(Id);
            if (resultFind == null ){
                return NotFound (); //status 404
            }
            result.MetaId = Id;
            _context.Metas.Update(result);
            await _context.SaveChangesAsync();

            return Ok(result);
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> Delete(int Id){
            Meta resultFind = await _context.Metas.FindAsync(Id);
            if (resultFind == null ){
                return NotFound (); //status 404
            }
            _context.Remove(resultFind);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}