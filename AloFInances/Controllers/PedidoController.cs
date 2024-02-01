using AloDoutor.Core.Messages;
using Azure.Core.Extensions;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace AloFinances.Api.Controllers
{
    [ApiController]
    [Route("/pedido")]
    public class PedidoController : ControllerBase
    {
        private readonly IBus _bus;

        public PedidoController(IBus bus)
        {
            _bus = bus;
        }

        [HttpPost("publish")]
        public async Task<IActionResult> Publish()
        {
            var nomeFila = "fila";
            // var endPoint = await _bus.GetSendEndpoint(new Uri($"queue:{nomeFila}"));
            //  await endPoint.Send(new AgendamentoCanceladoEvent(Guid.NewGuid()));
            await _bus.Publish(new AgendamentoCanceladoEvent(Guid.NewGuid()));          

            return Ok();
        }               
    }
}
