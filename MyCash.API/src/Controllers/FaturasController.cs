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
    public class FaturasController : ControllerBase
    {
        private readonly DataContext _context;
         public FaturasController(DataContext context){
         _context = context;
        }

        [HttpGet]
         public async Task<ActionResult<IEnumerable<Fatura>>> GetAll(){
            return await _context.Faturas.ToListAsync();
        }
        
        [HttpGet("{Id}")]
        public async Task<ActionResult<Fatura>> GetById(int Id){
            Fatura result = await _context.Faturas.FindAsync(Id);
            if(result==null){
                return NotFound();
            }
            return result;
            
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<Fatura>>> Post(Fatura result){
            await _context.Faturas.AddAsync(result);
            await _context.SaveChangesAsync();
             return Ok(result);
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult> Update(Fatura result, int Id){
            Fatura resultFind = await _context.Faturas.FindAsync(Id);
            if (resultFind == null ){
                return NotFound (); //status 404
            }
            result.FaturaId = Id;
            _context.Faturas.Update(result);
            await _context.SaveChangesAsync();

            return Ok(result);
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> Delete(int Id){
            Fatura resultFind = await _context.Faturas.FindAsync(Id);
            if (resultFind == null ){
                return NotFound (); //status 404
            }
            _context.Remove(resultFind);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}