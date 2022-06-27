using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyCash.API.Models;
using src.Data;
using src.Models;

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
            List<Receita> receitas = await _context.Receitas.ToListAsync();

            foreach(Receita receita in receitas){
                SubcategoriaReceita subcategoriaReceita = await _context.SubcategoriasReceitas.FindAsync(receita.SubcategoriaReceitaId);
                Conta conta = await _context.Contas.FindAsync(receita.ContaId);
                Banco banco = await _context.Bancos.FindAsync(receita.Conta.BancoId);
                CategoriaConta categoriaConta = await _context.CategoriasContas.FindAsync(receita.Conta.CategoriaContaId);
                Usuario usuario = await _context.Usuarios.FindAsync(receita.Conta.UsuarioId);
                
                receita.SubcategoriaReceita.CategoriaReceita = null;
                receita.Conta.Usuario =null;
                receita.Conta.DespesasConta = null;
                receita.Conta.CartoesCredito = null;
            }

            return await _context.Receitas.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Receita>> GetById(int id)
        {
            Receita result = await _context.Receitas.FindAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            SubcategoriaReceita subcategoriaReceita = await _context.SubcategoriasReceitas.FindAsync(result.SubcategoriaReceitaId);
            Conta conta = await _context.Contas.FindAsync(result.ContaId);

            result.SubcategoriaReceita.CategoriaReceita = null;
            result.Conta.Usuario =null;
            result.Conta.DespesasConta = null;
            result.Conta.CartoesCredito = null;


            return result;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Receita body)
        {
            Receita result = await _context.Receitas.FindAsync(id);
            if(result == null){
                return NotFound();
            }

            body.ReceitaId = id ;

            SubcategoriaReceita subcategoriaReceita = await _context.SubcategoriasReceitas.FindAsync(body.SubcategoriaReceitaId);
            body.SubcategoriaReceita = subcategoriaReceita;

            Conta conta = await _context.Contas.FindAsync(body.ContaId);
            body.Conta = conta;

            Banco banco = await _context.Bancos.FindAsync(body.Conta.BancoId);
            body.Conta.Banco = banco;

            _context.Entry<Receita>(result).State = EntityState.Detached;
            _context.Entry<Receita>(body).State = EntityState.Modified;
            
            _context.Receitas.Update(body);
            await _context.SaveChangesAsync();

           body.SubcategoriaReceita.CategoriaReceita =null;
            body.Conta.Usuario =null;
            body.Conta.DespesasConta = null;
            body.Conta.CartoesCredito = null;

            return Ok(body);
        }

        [HttpPost]
        public async Task<ActionResult<Receita>> Create(Receita body)
        {
            SubcategoriaReceita subcategoriaReceita = await _context.SubcategoriasReceitas.FindAsync(body.SubcategoriaReceitaId);
            body.SubcategoriaReceita = subcategoriaReceita;

            Conta conta = await _context.Contas.FindAsync(body.ContaId);
            body.Conta = conta;

            Banco banco = await _context.Bancos.FindAsync(body.Conta.BancoId);
            body.Conta.Banco = banco;
            
            await _context.Receitas.AddAsync(body);
            await _context.SaveChangesAsync();

            body.SubcategoriaReceita.CategoriaReceita =null;
            body.Conta.Usuario =null;
            body.Conta.DespesasConta = null;
            body.Conta.CartoesCredito = null;

            return Ok(body);
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