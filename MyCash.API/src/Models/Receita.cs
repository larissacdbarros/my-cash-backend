using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;
using src.Models.DTO;

namespace src.Models
{
    public class Receita
    {

        public int ReceitaId { get; set; }
        public string Descricao { get; set; }
        public int SubcategoriaReceitaId { get; set; }
        public SubcategoriaReceita SubcategoriaReceita { get; set; }
        public int ContaId { get; set; }
        public Conta Conta { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{MM/dd/yyyy}")]
        public DateTime Data { get; set; }
        public double Valor { get; set; }

        public Receita()
        {
            
        }
        
        public Receita(ReceitaReqDTO dto)
        {
            this.Descricao = dto.Descricao;
            this.SubcategoriaReceitaId = dto.SubcategoriaReceitaId;
            this.ContaId = dto.ContaId;
            this.Data = dto.Data;
            this.Valor = dto.Valor;
        }


    }
}