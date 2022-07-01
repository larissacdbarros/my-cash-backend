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
    [Route("api/Faturas")]
    public class FaturasController : ControllerBase
    {
        private readonly DataContext _context;
        public FaturasController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Fatura>>> GetAll()
        {
            return await _context.Faturas.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Fatura>> GetById(int id)
        {
            Fatura result = await _context.Faturas
            .Where(fatura => fatura.FaturaId == id)
            .FirstOrDefaultAsync();

            if (result == null)
            {
                return NotFound();
            }
            return result;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, FaturaReqDTO body)
        {
            Fatura result = await _context.Faturas.FindAsync(id);
            
            if(result == null){
                return NotFound();
            }

            Fatura fatura = new Fatura(body);
            fatura.FaturaId = id;

            _context.Entry<Fatura>(result).State = EntityState.Detached;
            _context.Entry<Fatura>(fatura).State = EntityState.Modified;
            
            _context.Faturas.Update(fatura);
            await _context.SaveChangesAsync();

            return Ok(fatura);
        }

        [HttpPost]
        public async Task<ActionResult<Fatura>> Create(FaturaReqDTO body)
        {
            Fatura fatura = new Fatura(body);
             
            
            await _context.Faturas.AddAsync(fatura);
            await _context.SaveChangesAsync();

            return Ok(body);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Fatura result = await _context.Faturas.FindAsync(id);
            if (result == null ){
                return NotFound (); 
            }
            _context.Remove(result);
            await _context.SaveChangesAsync();

            return Ok();

        }

        [HttpGet("{contaId}/FaturasAbertas")]
        public  IEnumerable<FaturaDTO> GetFaturasAbertas(int contaId)
        {

            var saldoFatura = from fatura in _context.Faturas
            join despesaCartao in _context.DespesasCartao on fatura.FaturaId equals despesaCartao.FaturaId 
            where fatura.ContaId == contaId
            && fatura.isFaturaPaga == false
            group despesaCartao by new {fatura.FaturaId, fatura.Mes, fatura.Ano } into g
            select new {
                
                faturaId = g.Key.FaturaId,
                mes = g.Key.Mes,
                ano = g.Key.Ano,
                valorFatura = g.Sum(s=> s.Valor)
            };



            List<FaturaDTO> list = new List<FaturaDTO>();
            saldoFatura.ToList().ForEach(l => {
                list.Add (new FaturaDTO(l.faturaId,  l.ano, l.mes, l.valorFatura)); 
            });

            return list;
            
        
        }

        [HttpPut("{id}/Pagar")]
        public async Task<IActionResult> PagarFatura(int id)
        {
            Fatura result = await _context.Faturas.FindAsync(id);

            if(result == null || result.isFaturaPaga == true){
                return NotFound();
            }

            if(!this.IsDataFaturaValida(result)){
                return NotFound();
            }
            


            //colocar fatura como paga
            result.isFaturaPaga = true ;

            //somar as despesas da fatura 
            double valorFatura = 0;

            var despesaCartao =  _context.DespesasCartao
                .Where(DespesaCartao => DespesaCartao.FaturaId == result.FaturaId)
                .ToList();
                result.DespesasCartao = despesaCartao;
            foreach(DespesaCartao despesa in result.DespesasCartao){
                valorFatura += despesa.Valor;
            }

            //criar uma receita com as despesas fatura
            DespesaConta despesaConta = new DespesaConta();
            despesaConta.ContaId = result.ContaId;
            despesaConta.Data = DateTime.Now;
            despesaConta.Descricao = $"Fatura {result.Mes}/{result.Ano}";
            despesaConta.Valor = valorFatura;
            despesaConta.SubcategoriaDespesaId = VerificarSubcategoriaDespesa();
           _context.DespesasConta.Add(despesaConta);
           
        
            _context.Faturas.Update(result);
            await _context.SaveChangesAsync();

            return Ok(result);
        }

        private bool IsDataFaturaValida(Fatura fatura){

            int mes = fatura.Mes;
            int ano = fatura.Ano;

            if(mes == 12){
                ano += 1;
                mes =1;
            }else{
                mes += 1;
            }

            DateTime dataFatura = new  DateTime(ano, mes, 1);
            DateTime dataAtual =   DateTime.Now;

            return dataAtual > dataFatura;
            }

        private int VerificarSubcategoriaDespesa(){
            
            var subcategoria = _context.SubcategoriasDespesas
                        .Where (sub => sub.Tipo == "Fatura" )
                        .FirstOrDefault();

            if(subcategoria != null){
                return subcategoria.SubcategoriaDespesaId;

            }else{
                CategoriaDespesa categoriaDespesa = new CategoriaDespesa();
                categoriaDespesa.Tipo = "Fatura";
                categoriaDespesa.Icone = "";

                _context.CategoriasDespesas.Add(categoriaDespesa);
                _context.SaveChanges();

                SubcategoriaDespesa subcategoriaDespesa = new SubcategoriaDespesa();
                subcategoriaDespesa.CategoriaDespesaId = categoriaDespesa.CategoriaDespesaId;
                subcategoriaDespesa.Tipo = "Fatura";
                subcategoriaDespesa.Icone = "";

                _context.SubcategoriasDespesas.Add(subcategoriaDespesa);
                _context.SaveChanges();

                return subcategoriaDespesa.SubcategoriaDespesaId;

            }
        }         
        
        
        }

        
        


    }
