using Microsoft.EntityFrameworkCore;
using MyCash.API.Models;
using src.Models;

namespace src.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios {get; set;}
        public DbSet<Banco> Bancos  {get; set;}
        public DbSet<BandeiraCartao> BandeirasCartoes  {get; set;}
        public DbSet<CartaoCredito> CartoesCredito  {get; set;}
        public DbSet<CategoriaConta> CategoriasContas {get; set;}
        public DbSet<CategoriaDespesa> CategoriasDespesas  {get; set;}
        public DbSet<CategoriaReceita> CategoriasReceitas  {get; set;}
        public DbSet<Conta> Contas {get; set;}
        public DbSet<Despesa> Despesas  {get; set;}
        public DbSet<Fatura> Faturas  {get; set;}
        public DbSet<Meta> Metas  {get; set;}
        public DbSet<Objetivo> Objetivos  {get; set;}
        public DbSet<Receita> Receitas  {get; set;}
        public DbSet<SubcategoriaDespesa> SubcategoriasDespesas  {get; set;}
        public DbSet<SubcategoriaReceita> SubcategoriasReceitas  {get; set;}


        
    }
}