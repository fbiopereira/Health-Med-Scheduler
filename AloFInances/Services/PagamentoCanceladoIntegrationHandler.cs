using AloDoutor.Core.Messages.Integration;
using AloFinances.Api.Application.Commands;
using MassTransit;
using MediatR;

namespace AloFinances.Api.Services
{
    public class PagamentoCanceladoIntegrationHandler : IConsumer<AgendamentoCanceladoEvent>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger _logger;

        public PagamentoCanceladoIntegrationHandler(IServiceProvider serviceProvider, ILogger<PagamentoCanceladoIntegrationHandler> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<AgendamentoCanceladoEvent> context)
        {
            await ProcessarPagamento(context.Message);
        }

        private async Task ProcessarPagamento(AgendamentoCanceladoEvent message)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                try
                {
                    var commandHandler = scope.ServiceProvider.GetRequiredService<IMediator>();
                    var command = new ContaCanceladaComand(message.IdAgendamento);
                    await commandHandler.Send(command);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "PagamentoCanceladoIntegrationHandler - Agendamento: {agendamento}", message);

                }
            }
        }
    }    
}
