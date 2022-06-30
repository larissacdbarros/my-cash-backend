using Microsoft.EntityFrameworkCore;
using MyCash.API.Models;
using src.Models;

namespace src.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) {

        }

        public DbSet<Usuario> Usuarios {get; set;}
        public DbSet<Banco> Bancos  {get; set;}
        public DbSet<CategoriaConta> CategoriasContas {get; set;}
        public DbSet<CategoriaDespesa> CategoriasDespesas  {get; set;}
        public DbSet<CategoriaReceita> CategoriasReceitas  {get; set;}
        public DbSet<Conta> Contas {get; set;}
        public DbSet<DespesaConta> DespesasConta  {get; set;}
         public DbSet<DespesaCartao> DespesasCartao  {get; set;}
        public DbSet<Fatura> Faturas  {get; set;}
        public DbSet<Receita> Receitas  {get; set;}
        public DbSet<SubcategoriaDespesa> SubcategoriasDespesas  {get; set;}
        public DbSet<SubcategoriaReceita> SubcategoriasReceitas  {get; set;}


        
    }
}