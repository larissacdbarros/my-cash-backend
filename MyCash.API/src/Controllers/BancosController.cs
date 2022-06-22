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
    public class BancosController : ControllerBase
    {
        private readonly DataContext _context;
        
        public BancosController(DataContext context){
         _context = context;
        }
        
        [HttpGet]
         public async Task<ActionResult<IEnumerable<Banco>>> GetAll(){
            return await _context.Bancos.ToListAsync();
        }
        
        [HttpGet("{Id}")]
        public async Task<ActionResult<Banco>> GetById(int Id){
            Banco result = await _context.Bancos.FindAsync(Id);
            if(result==null){
                return NotFound();
            }
            return result;
            
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<Banco>>> Post(Banco result){
            await _context.Bancos.AddAsync(result);
            await _context.SaveChangesAsync();
             return Ok(result);
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult> Update(Banco result, int Id){
            Banco resultFind = await _context.Bancos.FindAsync(Id);
            if (resultFind == null ){
                return NotFound (); //status 404
            }
            result.BancoId = Id;
            _context.Bancos.Update(result);
            await _context.SaveChangesAsync();

            return Ok(result);
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> Delete(int Id){
            Banco resultFind = await _context.Bancos.FindAsync(Id);
            if (resultFind == null ){
                return NotFound (); //status 404
            }
            _context.Remove(resultFind);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
