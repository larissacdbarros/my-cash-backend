using System;
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
    [Route("api/despesaCartao")]
    public class DespesasCartaoController : ControllerBase
    {
        private readonly DataContext _context;
    
        public DespesasCartaoController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DespesaCartao>>> GetAll()
        {
            return await _context.DespesasCartao
                .Include(despesaCartao => despesaCartao.Fatura)
                .Include(despesaCartao => despesaCartao.SubcategoriaDespesa)
                .ThenInclude(subcategoriaDespesa => subcategoriaDespesa.CategoriaDespesa)
                .Include(despesaCartao => despesaCartao.Fatura)
                .ToListAsync();

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DespesaCartao>> GetById(int id)
        {
            var result = await _context.DespesasCartao
                .Include(despesaCartao => despesaCartao.Fatura)
                .Include(despesaCartao => despesaCartao.SubcategoriaDespesa)
                .ThenInclude(subcategoriaDespesa => subcategoriaDespesa.CategoriaDespesa)
                .Include(despesaCartao => despesaCartao.Fatura)
                .Where(DespesaCartao => DespesaCartao.DespesaCartaoId == id)
                .FirstOrDefaultAsync();
            

            if (result == null)
            {
                return NotFound();
            }

            return result;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, DespesaCartaoReqDTO body)
        {
            DespesaCartao result = await _context.DespesasCartao.FindAsync(id);
            
            if(result == null){
                return NotFound();
            }

            DespesaCartao despesaCartao = new DespesaCartao(body);

            despesaCartao.DespesaCartaoId = id ;

            SubcategoriaDespesa subcategoriaDespesa = await _context.SubcategoriasDespesas.FindAsync(despesaCartao.SubcategoriaDespesaId);
            despesaCartao.SubcategoriaDespesa = subcategoriaDespesa;



            Conta conta = await _context.Contas.FindAsync(despesaCartao.Fatura.ContaId);
            despesaCartao.Fatura.Conta = conta;

            despesaCartao.Fatura = VerificarFatura(despesaCartao, body.ContaId);
            
            _context.Entry<DespesaCartao>(result).State = EntityState.Detached;
            _context.Entry<DespesaCartao>(despesaCartao).State = EntityState.Modified;
            
            _context.DespesasCartao.Update(despesaCartao);
            await _context.SaveChangesAsync();

            despesaCartao.SubcategoriaDespesa.CategoriaDespesa=null;
            despesaCartao.Fatura.DespesasCartao = null;
            despesaCartao.Fatura.Conta.DespesasConta = null;

            return Ok(despesaCartao);
        }

        [HttpPost]
        public async Task<ActionResult<DespesaCartao>> Create(DespesaCartaoReqDTO body)
        {
            DespesaCartao despesaCartao = new DespesaCartao(body);

            despesaCartao.Fatura = VerificarFatura(despesaCartao, body.ContaId);

            SubcategoriaDespesa subcategoriaDespesa = await _context.SubcategoriasDespesas.FindAsync(despesaCartao.SubcategoriaDespesaId);
            despesaCartao.SubcategoriaDespesa = subcategoriaDespesa;

            CategoriaDespesa categoriaDespesa = await _context.CategoriasDespesas.FindAsync(despesaCartao.SubcategoriaDespesa.CategoriaDespesaId);
            despesaCartao.SubcategoriaDespesa.CategoriaDespesa = categoriaDespesa;

            Conta conta = await _context.Contas.FindAsync(despesaCartao.Fatura.ContaId);
            despesaCartao.Fatura.Conta = conta;

            await _context.DespesasCartao.AddAsync(despesaCartao);
            await _context.SaveChangesAsync();

           // despesaCartao.SubcategoriaDespesa.CategoriaDespesa.SubcategoriasDespesa=null;

            despesaCartao.Fatura.DespesasCartao = null;
            despesaCartao.Fatura.Conta.DespesasConta = null;
            
            return Ok(despesaCartao);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            DespesaCartao result = await _context.DespesasCartao.FindAsync(id);
            if (result == null ){
                return NotFound (); 
            }
            _context.Remove(result);
            await _context.SaveChangesAsync();

            return Ok();

        }

        private Fatura VerificarFatura(DespesaCartao despesaCartao, int contaId){
            int mes = despesaCartao.Data.Month;
            int ano = despesaCartao.Data.Year;

            int mesFechamento = despesaCartao.Data.Month;
            int anoFechamento = despesaCartao.Data.Year;
                
             if(mesFechamento == 12){
                anoFechamento += 1;
                mesFechamento =1;
            }else{
                mesFechamento += 1;
            }

            var fatura = _context.Faturas
                .Where(fatura => fatura.Mes == mes && fatura.Ano == ano && fatura.ContaId == contaId)
                .FirstOrDefault();

            if(fatura != null){
                return fatura;                      

            }else{
                Fatura novaFatura = new Fatura();
                novaFatura.Ano = ano;
                novaFatura.Mes = mes;
                novaFatura.DataFechamentoFatura = new DateTime(anoFechamento, mesFechamento, 1); //fatura fecha todo dia primeiro
                novaFatura.ContaId = contaId;

                return novaFatura;

            }       
        }

    }

    

    
}