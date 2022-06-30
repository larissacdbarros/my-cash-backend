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
    [Route("api/[controller]")]
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
                .Include(despesaCartao => despesaCartao.CartaoCredito)
                .Include(despesaCartao => despesaCartao.SubcategoriaDespesa)
                .ThenInclude(subcategoriaDespesa => subcategoriaDespesa.CategoriaDespesa)
                .Include(despesaCartao => despesaCartao.Fatura)
                .ToListAsync();

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DespesaCartao>> GetById(int id)
        {
            var result = await _context.DespesasCartao
                .Include(despesaCartao => despesaCartao.CartaoCredito)
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

            CartaoCredito cartaoCredito = await _context.CartoesCredito.FindAsync(despesaCartao.CartaoCreditoId);
            despesaCartao.CartaoCredito = cartaoCredito;

            BandeiraCartao bandeiraCartao = await _context.BandeirasCartoes.FindAsync(despesaCartao.CartaoCredito.BandeiraCartaoId);
            despesaCartao.CartaoCredito.BandeiraCartao = bandeiraCartao;

            Conta conta = await _context.Contas.FindAsync(despesaCartao.CartaoCredito.ContaId);
            despesaCartao.CartaoCredito.Conta = conta;

            despesaCartao.Fatura = VerificarFatura(despesaCartao);
            
            _context.Entry<DespesaCartao>(result).State = EntityState.Detached;
            _context.Entry<DespesaCartao>(despesaCartao).State = EntityState.Modified;
            
            _context.DespesasCartao.Update(despesaCartao);
            await _context.SaveChangesAsync();

            despesaCartao.SubcategoriaDespesa.CategoriaDespesa=null;
            despesaCartao.CartaoCredito.DespesasCartao = null;
            despesaCartao.CartaoCredito.Conta.DespesasConta = null;
            despesaCartao.CartaoCredito.Conta.CartoesCredito = null;


            return Ok(despesaCartao);
        }

        [HttpPost]
        public async Task<ActionResult<DespesaCartao>> Create(DespesaCartaoReqDTO body)
        {
            DespesaCartao despesaCartao = new DespesaCartao(body);

            SubcategoriaDespesa subcategoriaDespesa = await _context.SubcategoriasDespesas.FindAsync(despesaCartao.SubcategoriaDespesaId);
            despesaCartao.SubcategoriaDespesa = subcategoriaDespesa;

            CartaoCredito cartaoCredito = await _context.CartoesCredito.FindAsync(despesaCartao.CartaoCreditoId);
            despesaCartao.CartaoCredito = cartaoCredito;

            BandeiraCartao bandeiraCartao = await _context.BandeirasCartoes.FindAsync(despesaCartao.CartaoCredito.BandeiraCartaoId);
            despesaCartao.CartaoCredito.BandeiraCartao = bandeiraCartao;

            Conta conta = await _context.Contas.FindAsync(despesaCartao.CartaoCredito.ContaId);
            despesaCartao.CartaoCredito.Conta = conta;

            despesaCartao.Fatura = VerificarFatura(despesaCartao);

            await _context.DespesasCartao.AddAsync(despesaCartao);
            await _context.SaveChangesAsync();

            despesaCartao.SubcategoriaDespesa.CategoriaDespesa=null;

            despesaCartao.CartaoCredito.DespesasCartao = null;
            despesaCartao.CartaoCredito.Conta.DespesasConta = null;
            despesaCartao.CartaoCredito.Conta.CartoesCredito = null;
            
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

        public Fatura VerificarFatura(DespesaCartao despesaCartao){
            int mes = despesaCartao.Data.Month;
            int ano = despesaCartao.Data.Year;

            var fatura = _context.Faturas
                .Where(fatura => fatura.Mes == mes && fatura.Ano == ano)
                .FirstOrDefault();

            if(fatura != null){
                return fatura;                      

            }else{
                Fatura novaFatura = new Fatura();
                novaFatura.Ano = ano;
                novaFatura.Mes = mes;
                novaFatura.DataFechamentoFatura = new DateTime(ano, mes, 1); //fatura fecha todo dia primeiro
                return novaFatura;
            }       
        }

    }

    

    
}