using AloDoutor.Core.Messages;
using MassTransit;

namespace Relatorio.API.Services
{
    public class RelatorioIntegratorHandler : IConsumer<RelatorioEvent>
    {
        public async Task Consume(ConsumeContext<RelatorioEvent> context)
        {
            await Task.CompletedTask;

            return;
        }
    }
}
