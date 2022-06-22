using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using src.Data;
using src.Models;

namespace MyCash.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly DataContext _context;

         public UsuariosController(DataContext context){
         _context = context;
        }

        [HttpGet]
         public async Task<ActionResult<IEnumerable<Usuario>>> GetAll(){
            return await _context.Usuarios.ToListAsync();
        }
        
        [HttpGet("{Id}")]
        public async Task<ActionResult<Usuario>> GetById(int Id){
            Usuario result = await _context.Usuarios.FindAsync(Id);
            if(result==null){
                return NotFound();
            }
            return result;
            
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<Usuario>>> Post(Usuario result){
            await _context.Usuarios.AddAsync(result);
            await _context.SaveChangesAsync();
             return Ok(result);
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult> Update(Usuario result, int Id){
            Usuario resultFind = await _context.Usuarios.FindAsync(Id);
            if (resultFind == null ){
                return NotFound (); //status 404
            }
            result.UsuarioId = Id;
            _context.Usuarios.Update(result);
            await _context.SaveChangesAsync();

            return Ok(result);
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> Delete(int Id){
            Usuario resultFind = await _context.Usuarios.FindAsync(Id);
            if (resultFind == null ){
                return NotFound (); //status 404
            }
            _context.Remove(resultFind);
            await _context.SaveChangesAsync();
            return Ok();
        }
        
    }
}