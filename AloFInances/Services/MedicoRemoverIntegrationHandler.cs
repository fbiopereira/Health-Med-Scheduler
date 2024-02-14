using AloDoutor.Core.Messages.Integration;
using AloFinances.Api.Application.Commands;
using MassTransit;
using MediatR;

namespace AloFinances.Api.Services
{
    public class MedicoRemoverIntegrationHandler : IConsumer<MedicoRemovidoEvent>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger _logger;

        public MedicoRemoverIntegrationHandler(IServiceProvider serviceProvider, ILogger<MedicoRemoverIntegrationHandler> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<MedicoRemovidoEvent> context)
        {
            await ProcessarMedico(context.Message);
        }

        private async Task ProcessarMedico(MedicoRemovidoEvent message)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                try
                {
                    var commandHandler = scope.ServiceProvider.GetRequiredService<IMediator>();
                    var command = new MedicoRemovidoComand(message.CpfMedico, message.Crm);
                    await commandHandler.Send(command);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "PacienteIntegrationHandler: {Nome}, CPF: {CPF}", message.CpfMedico);

                }
            }
        }
    }
}
