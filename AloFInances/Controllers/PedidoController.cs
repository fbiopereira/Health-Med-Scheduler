using Microsoft.AspNetCore.Mvc;

namespace AloFinances.Api.Controllers
{
    [ApiController]
    [Route("/financeiro")]
    public class PedidoController : ControllerBase
    {

        public PedidoController()
        {
        }

        [HttpPost("status-api")]
        public IActionResult StatusAPI()
        {  
            return Ok("OK");
        }               
    }
}
