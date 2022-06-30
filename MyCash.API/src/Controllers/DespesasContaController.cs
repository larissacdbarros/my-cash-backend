using System.Collections.Generic;
using System.Linq;
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

             
            return await _context.DespesasConta
                .Include(despesasConta => despesasConta.SubcategoriaDespesa)
                .ThenInclude(subcategoriaDepesa => subcategoriaDepesa.CategoriaDespesa)
                .Include(SubcategoriaDespesa => SubcategoriaDespesa.Conta).ThenInclude(conta => conta.Banco)
                .Include(SubcategoriaDespesa => SubcategoriaDespesa.Conta).ThenInclude(conta => conta.Usuario)
                .ToListAsync();
                
               
           
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DespesaConta>> GetById(int id)
        {

            var result = await _context.DespesasConta
            .Include(DespesaConta => DespesaConta.SubcategoriaDespesa)
            .ThenInclude(SubcategoriaDespesa => SubcategoriaDespesa.CategoriaDespesa)
            .Include(DespesaConta => DespesaConta.Conta).ThenInclude(conta => conta.Banco)
            .Include(DespesaConta => DespesaConta.Conta).ThenInclude(conta => conta.Usuario)
            .Where(DespesaConta => DespesaConta.DespesaContaId == id)
            .FirstOrDefaultAsync();

            if (result == null)
            {
                return NotFound();
            }
            return result;
            
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, DespesaContaReqDTO body)
        {
            DespesaConta result = await _context.DespesasConta.FindAsync(id);
            if(result == null){
                return NotFound();
            }
            DespesaConta despesaConta = new DespesaConta(body);

            despesaConta.DespesaContaId = id ;

            SubcategoriaDespesa subcategoriaDespesa = await _context.SubcategoriasDespesas.FindAsync(body.SubcategoriaDespesaId);
            despesaConta.SubcategoriaDespesa = subcategoriaDespesa;

            Conta conta = await _context.Contas.FindAsync(despesaConta.ContaId);
            despesaConta.Conta = conta;

            Banco banco = await _context.Bancos.FindAsync(despesaConta.Conta.BancoId);
            despesaConta.Conta.Banco = banco;
           
           
            _context.Entry<DespesaConta>(result).State = EntityState.Detached;
            _context.Entry<DespesaConta>(despesaConta).State = EntityState.Modified;
            
            _context.DespesasConta.Update(despesaConta);
            await _context.SaveChangesAsync();

            despesaConta.SubcategoriaDespesa.CategoriaDespesa =null;
            despesaConta.Conta.Usuario =null;
            despesaConta.Conta.DespesasConta = null;
            despesaConta.Conta.CartoesCredito = null;

            return Ok(despesaConta);
        }

        [HttpPost]
        public async Task<ActionResult<DespesaConta>> Create(DespesaContaReqDTO body)
        {
            DespesaConta despesaConta = new DespesaConta(body);

            SubcategoriaDespesa subcategoriaDespesa = await _context.SubcategoriasDespesas.FindAsync(despesaConta.SubcategoriaDespesaId);
             despesaConta.SubcategoriaDespesa = subcategoriaDespesa;

            Conta conta = await _context.Contas.FindAsync(despesaConta.ContaId);
            despesaConta.Conta = conta;

            Banco banco = await _context.Bancos.FindAsync(despesaConta.Conta.BancoId);
            despesaConta.Conta.Banco = banco;

            await _context.DespesasConta.AddAsync(despesaConta);
            await _context.SaveChangesAsync();

            despesaConta.SubcategoriaDespesa.CategoriaDespesa =null;
            despesaConta.Conta.Usuario =null;
            despesaConta.Conta.DespesasConta = null;
            despesaConta.Conta.CartoesCredito = null;

            return Ok(despesaConta);
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