using System.Collections.Generic;

namespace src.Models
{
    public class CategoriaDespesa
    {
        public int CategoriaDespesaId { get; set; }
        public string Tipo { get; set; }
        public string Icone { get; set; }
        public IEnumerable<SubcategoriaDespesa> SubcategoriasDespesa { get; set; }

    }
}