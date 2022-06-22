using Microsoft.AspNetCore.Mvc;

namespace src.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartoesCreditoController
    {
        [HttpGet]
         public string GetAll(){
            return "Teste";
            //await -- aguardando
            //task -- tarefa executar uma tarefa
            // actipon result gera uma saída v ou f 
         }
    }
}