using AloDoutor.Core.Messages.Integration;
using AloFinances.Api.Application.Commands;
using MassTransit;
using MediatR;

namespace AloFinances.Api.Services
{
    public class PagamentoRealizadoIntegrationHandler : IConsumer<AgendamentoRealizadoEvent>
    {

        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger _logger;

        public PagamentoRealizadoIntegrationHandler(IServiceProvider serviceProvider, ILogger<PagamentoRealizadoIntegrationHandler> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<AgendamentoRealizadoEvent> context)
        {
            await ProcessarPagamento(context.Message);
        }

        private async Task ProcessarPagamento(AgendamentoRealizadoEvent message)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                try
                {
                    var commandHandler = scope.ServiceProvider.GetRequiredService<IMediator>();
                    var command = new ContaCommand(message.IdAgendamento, message.DataAgendamento, message.PacienteCPF, message.MedicoCRM);
                    await commandHandler.Send(command);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "PagamentoRealizadoIntegrationHandler - Agendamento: {agendamento}", message);

                }
            }
        }
    }
}
