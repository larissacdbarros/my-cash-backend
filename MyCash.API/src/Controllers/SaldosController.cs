using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using src.Data;
using src.Models;
using src.Models.DTO;

namespace src.Controllers
{
    [ApiController]
    [Route("api/saldos")]
    public class SaldosController : ControllerBase
    {
        private readonly DataContext _context;
        public SaldosController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("{contaId}")]
        public SaldoDTO GetSaldos(int contaId){

            SaldoDTO saldoDTO = new SaldoDTO();

            var saldoReceita =  from receita in _context.Receitas
            where receita.ContaId == contaId
            group receita by receita.ContaId into g
            select new {
                
                saldo = g.Sum(s=> s.Valor) 
            };

            if(saldoReceita.SingleOrDefault() != null){
                saldoDTO.SaldoReceita = saldoReceita.SingleOrDefault().saldo;
            }

            
            var saldoDespesa =  from despesaConta in _context.DespesasConta
            where despesaConta.ContaId == contaId
            group despesaConta by despesaConta.ContaId into g
            select new {
                
                saldo = g.Sum(s=> s.Valor) 
            };

            if(saldoDespesa.SingleOrDefault() != null){
                saldoDTO.SaldoDespesa = saldoDespesa.SingleOrDefault().saldo;
            }

            DateTime dataAtual = DateTime.Now;
            int mes = dataAtual.Month;
            int ano = dataAtual.Year;
         

            var totalFaturaCartao =  from despesaCartao in _context.DespesasCartao
            join fatura in _context.Faturas on despesaCartao.FaturaId equals fatura.FaturaId
            where fatura.Mes == mes && fatura.Ano == ano && fatura.ContaId == contaId
            group despesaCartao by fatura.FaturaId into g
            select new {
                
                saldo = g.Sum(s=> s.Valor) 
            };

            if(totalFaturaCartao.SingleOrDefault() != null){
                saldoDTO.TotalFaturaCartao = totalFaturaCartao.SingleOrDefault().saldo;
            }

            saldoDTO.SaldoAtual = saldoDTO.SaldoReceita - saldoDTO.SaldoDespesa;

            return saldoDTO; 

        }
        
    }
}