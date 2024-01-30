using AloDoutor.Core.Messages;
using MassTransit;

namespace AloFInances.Services
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
