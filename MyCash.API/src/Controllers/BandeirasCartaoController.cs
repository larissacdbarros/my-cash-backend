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
    public class BandeirasCartaoController : ControllerBase
    {
        private readonly DataContext _context;
        
         public BandeirasCartaoController(DataContext context){
         _context = context;
        }

        [HttpGet]
         public async Task<ActionResult<IEnumerable<BandeiraCartao>>> GetAll(){
            return await _context.BandeirasCartoes.ToListAsync();
        }
        
        [HttpGet("{Id}")]
        public async Task<ActionResult<BandeiraCartao>> GetById(int Id){
            BandeiraCartao result = await _context.BandeirasCartoes.FindAsync(Id);
            if(result==null){
                return NotFound();
            }
            return result;
            
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<BandeiraCartao>>> Post(BandeiraCartao result){
            await _context.BandeirasCartoes.AddAsync(result);
            await _context.SaveChangesAsync();
             return Ok(result);
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult> Update(BandeiraCartao result, int Id){
            BandeiraCartao resultFind = await _context.BandeirasCartoes.FindAsync(Id);
            if (resultFind == null ){
                return NotFound (); //status 404
            }
            result.BandeiraCartaoId = Id;
            _context.BandeirasCartoes.Update(result);
            await _context.SaveChangesAsync();

            return Ok(result);
        }

         [HttpDelete("{Id}")]
        public async Task<ActionResult> Delete(int Id){
            BandeiraCartao resultFind = await _context.BandeirasCartoes.FindAsync(Id);
            if (resultFind == null ){
                return NotFound (); //status 404
            }
            _context.Remove(resultFind);
            await _context.SaveChangesAsync();
            return Ok();
        }

        
    }
}