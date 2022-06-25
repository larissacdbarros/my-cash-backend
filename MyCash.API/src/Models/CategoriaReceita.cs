using System.Collections.Generic;

namespace src.Models
{
    public class CategoriaReceita
    {
        public int CategoriaReceitaId { get; set; }
        public string Tipo { get; set; }    
        public string Icone { get; set; }
       public IEnumerable<SubcategoriaReceita> SubcategoriasReceita { get; set; }

    }
}