using AloDoutor.Core.Messages.Integration;
using AloFinances.Api.Application.Commands;
using MassTransit;
using MediatR;

namespace AloFinances.Api.Services
{
    public class PacienteRemoverIntegrationHandler : IConsumer<PacienteRemovidoEvent>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger _logger;

        public PacienteRemoverIntegrationHandler(IServiceProvider serviceProvider, ILogger<PacienteRemoverIntegrationHandler> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<PacienteRemovidoEvent> context)
        {
            await ProcessarPaciente(context.Message);
        }

        private async Task ProcessarPaciente(PacienteRemovidoEvent message)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                try
                {
                    var commandHandler = scope.ServiceProvider.GetRequiredService<IMediator>();
                    var command = new PacienteRemovidoComand(message.CpfPaciente);
                    await commandHandler.Send(command);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "PacienteIntegrationHandler: {Nome}, CPF: {CPF}", message.CpfPaciente);

                }
            }
        }
    }
}
