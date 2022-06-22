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
    public class ContasController : ControllerBase
    {
        private readonly DataContext _context;
         public ContasController(DataContext context){
         _context = context;
        }

        [HttpGet]
         public async Task<ActionResult<IEnumerable<Conta>>> GetAll(){
            return await _context.Contas.ToListAsync();
        }
        
        [HttpGet("{Id}")]
        public async Task<ActionResult<Conta>> GetById(int Id){
            Conta result = await _context.Contas.FindAsync(Id);
            if(result==null){
                return NotFound();
            }
            return result;
            
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<Conta>>> Post(Conta result){
            await _context.Contas.AddAsync(result);
            await _context.SaveChangesAsync();
             return Ok(result);
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult> Update(Conta result, int Id){
            Conta resultFind = await _context.Contas.FindAsync(Id);
            if (resultFind == null ){
                return NotFound (); //status 404
            }
            result.ContaId = Id;
            _context.Contas.Update(result);
            await _context.SaveChangesAsync();

            return Ok(result);
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> Delete(int Id){
            Conta resultFind = await _context.Contas.FindAsync(Id);
            if (resultFind == null ){
                return NotFound (); //status 404
            }
            _context.Remove(resultFind);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}