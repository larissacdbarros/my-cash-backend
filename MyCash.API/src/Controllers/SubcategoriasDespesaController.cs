using Microsoft.AspNetCore.Mvc;
using src.Data;

namespace src.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubcategoriasDespesaController : ControllerBase
    {
        private readonly DataContext _context;

        [HttpGet]
         public string GetAll(){
            return "Teste";
            //await -- aguardando
            //task -- tarefa executar uma tarefa
            // actipon result gera uma saída v ou f 
         }
    }
}