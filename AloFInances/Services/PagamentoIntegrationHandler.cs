using AloDoutor.Core.Messages.Integration;
using MassTransit;

namespace AloFinances.Api.Services
{
    public class PagamentoRealizadoIntegrationHandler : IConsumer<AgendamentoRealizadoEvent>
    {
        public async Task Consume(ConsumeContext<AgendamentoRealizadoEvent> context)
        {
            await Task.CompletedTask;

            return;
        }
    }
}
