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
    public class DespesasContaController : ControllerBase
    {
        private readonly DataContext _context;
       public DespesasContaController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DespesaConta>>> GetAll()
        {
            List<DespesaConta> despesasConta = await _context.DespesasConta.ToListAsync();

            foreach(DespesaConta despesa in despesasConta){
                SubcategoriaDespesa subcategoriaDespesa = await _context.SubcategoriasDespesas.FindAsync(despesa.SubcategoriaDespesaId);
                Conta conta = await _context.Contas.FindAsync(despesa.ContaId);
                Banco banco = await _context.Bancos.FindAsync(despesa.Conta.BancoId);
                CategoriaConta categoriaConta = await _context.CategoriasContas.FindAsync(despesa.Conta.CategoriaContaId);
                Usuario usuario = await _context.Usuarios.FindAsync(despesa.Conta.UsuarioId);
                
                despesa.SubcategoriaDespesa.CategoriaDespesa = null;
                despesa.Conta.Usuario =null;
                despesa.Conta.DespesasConta = null;
                despesa.Conta.CartoesCredito = null;
            }

            return await _context.DespesasConta.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DespesaConta>> GetById(int id)
        {
            DespesaConta result = await _context.DespesasConta.FindAsync(id);

            if (result == null)
            {
                return NotFound();
            }
            
            SubcategoriaDespesa subcategoriaDespesa = await _context.SubcategoriasDespesas.FindAsync(result.SubcategoriaDespesaId);
            Conta conta = await _context.Contas.FindAsync(result.ContaId);

            result.SubcategoriaDespesa.CategoriaDespesa = null;
            result.Conta.Usuario =null;
            result.Conta.DespesasConta = null;
            result.Conta.CartoesCredito = null;
            
            return result;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, DespesaConta body)
        {
            DespesaConta result = await _context.DespesasConta.FindAsync(id);
            if(result == null){
                return NotFound();
            }

            body.DespesaContaId = id ;

            SubcategoriaDespesa subcategoriaDespesa = await _context.SubcategoriasDespesas.FindAsync(body.SubcategoriaDespesaId);
            body.SubcategoriaDespesa = subcategoriaDespesa;

            Conta conta = await _context.Contas.FindAsync(body.ContaId);
            body.Conta = conta;

            Banco banco = await _context.Bancos.FindAsync(body.Conta.BancoId);
            body.Conta.Banco = banco;
           
           
            _context.Entry<DespesaConta>(result).State = EntityState.Detached;
            _context.Entry<DespesaConta>(body).State = EntityState.Modified;
            
            _context.DespesasConta.Update(body);
            await _context.SaveChangesAsync();

            body.SubcategoriaDespesa.CategoriaDespesa =null;
            body.Conta.Usuario =null;
            body.Conta.DespesasConta = null;
            body.Conta.CartoesCredito = null;

            return Ok(body);
        }

        [HttpPost]
        public async Task<ActionResult<DespesaConta>> Create(DespesaConta body)
        {
            SubcategoriaDespesa subcategoriaDespesa = await _context.SubcategoriasDespesas.FindAsync(body.SubcategoriaDespesaId);
            body.SubcategoriaDespesa = subcategoriaDespesa;

            Conta conta = await _context.Contas.FindAsync(body.ContaId);
            body.Conta = conta;

            Banco banco = await _context.Bancos.FindAsync(body.Conta.BancoId);
            body.Conta.Banco = banco;

            await _context.DespesasConta.AddAsync(body);
            await _context.SaveChangesAsync();

            body.SubcategoriaDespesa.CategoriaDespesa =null;
            body.Conta.Usuario =null;
            body.Conta.DespesasConta = null;
            body.Conta.CartoesCredito = null;

            return Ok(body);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            DespesaConta result = await _context.DespesasConta.FindAsync(id);
            if (result == null ){
                return NotFound (); 
            }
            _context.Remove(result);
            await _context.SaveChangesAsync();

            return Ok();

        }
    }
}