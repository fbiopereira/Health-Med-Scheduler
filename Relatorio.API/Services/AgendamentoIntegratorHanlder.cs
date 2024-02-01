using AloDoutor.Core.Messages;
using MassTransit;

namespace Relatorio.API.Services
{
    public class AgendamentoIntegratorHanlder : IConsumer<AgendamentoCanceladoEvent>
    {
        public async Task Consume(ConsumeContext<AgendamentoCanceladoEvent> context)
        {
            await Task.CompletedTask;

            return;
        }
    }
}
