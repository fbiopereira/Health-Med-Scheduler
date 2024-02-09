using AloDoutor.Core.Messages.Integration;
using MassTransit;

namespace AloFinances.Api.Services
{
    public class PagamentoCanceladoIntegrationHandler : IConsumer<AgendamentoCanceladoEvent>
    {
        public async Task Consume(ConsumeContext<AgendamentoCanceladoEvent> context)
        {
            await Task.CompletedTask;

            return;
        }
    }    
}
