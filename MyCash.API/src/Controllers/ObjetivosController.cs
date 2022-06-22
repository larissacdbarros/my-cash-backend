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
    public class ObjetivosController : ControllerBase
    {
        private readonly DataContext _context;
         public ObjetivosController(DataContext context){
         _context = context;
        }

        [HttpGet]
         public async Task<ActionResult<IEnumerable<Objetivo>>> GetAll(){
            return await _context.Objetivos.ToListAsync();
        }
        
        [HttpGet("{Id}")]
        public async Task<ActionResult<Objetivo>> GetById(int Id){
            Objetivo result = await _context.Objetivos.FindAsync(Id);
            if(result==null){
                return NotFound();
            }
            return result;
            
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<Objetivo>>> Post(Objetivo result){
            await _context.Objetivos.AddAsync(result);
            await _context.SaveChangesAsync();
             return Ok(result);
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult> Update(Objetivo result, int Id){
            Objetivo resultFind = await _context.Objetivos.FindAsync(Id);
            if (resultFind == null ){
                return NotFound (); //status 404
            }
            result.ObjetivoId = Id;
            _context.Objetivos.Update(result);
            await _context.SaveChangesAsync();

            return Ok(result);
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> Delete(int Id){
            Objetivo resultFind = await _context.Objetivos.FindAsync(Id);
            if (resultFind == null ){
                return NotFound (); //status 404
            }
            _context.Remove(resultFind);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}