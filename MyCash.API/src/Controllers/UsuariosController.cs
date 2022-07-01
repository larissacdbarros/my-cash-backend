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

        public UsuariosController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetAll()
        {
            return await _context.Usuarios.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetById(int id)
        {
            Usuario result = await _context.Usuarios.FindAsync(id);

            if (result == null)
            {
                return NotFound();
            }
            return result;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Usuario body)
        {
            Usuario result = await _context.Usuarios.FindAsync(id);
            if(result == null){
                return NotFound();
            }

            body.UsuarioId = id ;
            

            _context.Entry<Usuario>(result).State = EntityState.Detached;
            _context.Entry<Usuario>(body).State = EntityState.Modified;
            
            _context.Usuarios.Update(body);
            await _context.SaveChangesAsync();

            return Ok(body);
        }

        [HttpPost]
        public async Task<ActionResult<Usuario>> Create(Usuario body)
        {
            await _context.Usuarios.AddAsync(body);
            await _context.SaveChangesAsync();

            return Ok(body);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Usuario result = await _context.Usuarios.FindAsync(id);
            if (result == null ){
                return NotFound (); 
            }
            _context.Remove(result);
            await _context.SaveChangesAsync();

            return Ok();

        }
    }
}