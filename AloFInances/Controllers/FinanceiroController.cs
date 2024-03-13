using Microsoft.AspNetCore.Mvc;

namespace AloFinances.Api.Controllers
{
    [ApiController]
    [Route("/financeiro")]
    public class FinanceiroController : ControllerBase
    {

        public FinanceiroController()
        {
        }

        [HttpPost("status-api")]
        public IActionResult StatusAPI()
        {  
            return Ok("OK");
        }               
    }
}
