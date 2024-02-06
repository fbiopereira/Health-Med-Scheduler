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
        private readonly ILogger _logger;

        public PedidoController(IBus bus, ILogger<string> logger)
        {
            _bus = bus;
            _logger = logger;
        }

        [HttpPost("publish")]
        public IActionResult Publish()
        {
            Task.Run(async () =>
            {
                for (int i = 0; i < 100000; i++) // Envie 100.000 mensagens para estressar a fila
                {
                    Guid id = Guid.NewGuid();
                    await _bus.Publish(new RelatorioEvent(id));
                    _logger.LogInformation($"{i} Mensagem publicada: {id}");
                }
            });

            return Ok();
        }               
    }
}
