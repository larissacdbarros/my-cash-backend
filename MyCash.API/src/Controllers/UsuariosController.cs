using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using src.Models;

namespace MyCash.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
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