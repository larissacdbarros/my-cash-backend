using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyCash.API.Models;
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
            CategoriaReceita categoriaReceita = await _context.CategoriasReceitas.FindAsync(result.SubcategoriaReceita.CategoriaReceitaId);


            Conta conta = await _context.Contas.FindAsync(result.ContaId);

            

            result.SubcategoriaReceita.CategoriaReceita.SubcategoriasReceita = null;
            result.Conta.Usuario =null;
            result.Conta.DespesasConta = null;
            result.Conta.CartoesCredito = null;

            


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