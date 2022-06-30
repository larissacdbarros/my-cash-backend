using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using src.Data;
using src.Models;
using src.Models.DTO;

namespace src.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReceitasController : ControllerBase
    {
        private readonly DataContext _context;
        public ReceitasController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Receita>>> GetAll()
        {
            return await _context.Receitas
                .Include(receita => receita.SubcategoriaReceita)
                .ThenInclude(subcategoriaReceita => subcategoriaReceita.CategoriaReceita)
                .Include(receita => receita.Conta)
                .ThenInclude(conta => conta.Banco)
                .Include(receita => receita.Conta)
                .ThenInclude(conta => conta.Usuario)
                .ToListAsync();

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Receita>> GetById(int id)
        {
            var result = await _context.Receitas
                .Include(receita => receita.SubcategoriaReceita)
                .ThenInclude(subcategoriaReceita => subcategoriaReceita.CategoriaReceita)
                .Include(receita => receita.Conta).ThenInclude(conta => conta.Banco)
                .Include(receita => receita.Conta).ThenInclude(conta => conta.Usuario)
                .Where(receita => receita.ReceitaId == id)
                .FirstOrDefaultAsync();
            

            if (result == null)
            {
                return NotFound();
            }

            return result;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ReceitaReqDTO body)
        {
            Receita result = await _context.Receitas.FindAsync(id);
            
            if(result == null){
                return NotFound();
            }

            Receita receita = new Receita(body); 

            receita.ReceitaId = id ;


            SubcategoriaReceita subcategoriaReceita = await _context.SubcategoriasReceitas.FindAsync(receita.SubcategoriaReceitaId);
            receita.SubcategoriaReceita = subcategoriaReceita;

            Conta conta = await _context.Contas.FindAsync(receita.ContaId);
            receita.Conta = conta;

            Banco banco = await _context.Bancos.FindAsync(receita.Conta.BancoId);
            receita.Conta.Banco = banco;

            _context.Entry<Receita>(result).State = EntityState.Detached;
            _context.Entry<Receita>(receita).State = EntityState.Modified;
            
            _context.Receitas.Update(receita);
            await _context.SaveChangesAsync();

            receita.SubcategoriaReceita.CategoriaReceita =null;
            receita.Conta.Usuario =null;
            receita.Conta.DespesasConta = null;
            receita.Conta.CartoesCredito = null;

            return Ok(receita);
        }

        [HttpPost]
        public async Task<ActionResult<Receita>> Create(ReceitaReqDTO body)
        {
            Receita receita = new Receita(body); 

            SubcategoriaReceita subcategoriaReceita = await _context.SubcategoriasReceitas.FindAsync(receita.SubcategoriaReceitaId);
            receita.SubcategoriaReceita = subcategoriaReceita;

            Conta conta = await _context.Contas.FindAsync(receita.ContaId);
            receita.Conta = conta;

            Banco banco = await _context.Bancos.FindAsync(receita.Conta.BancoId);
            receita.Conta.Banco = banco;
            
            await _context.Receitas.AddAsync(receita);
            await _context.SaveChangesAsync();

            receita.SubcategoriaReceita.CategoriaReceita =null;
            receita.Conta.Usuario =null;
            receita.Conta.DespesasConta = null;
            receita.Conta.CartoesCredito = null;

            return Ok(receita);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Receita result = await _context.Receitas.FindAsync(id);
            if (result == null ){
                return NotFound (); 
            }
            _context.Remove(result);
            await _context.SaveChangesAsync();

            return Ok();

        }
    }
}