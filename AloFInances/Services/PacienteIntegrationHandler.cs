using AloDoutor.Core.Messages.Integration;
using AloFinances.Api.Application.Commands;
using MassTransit;
using MediatR;

namespace AloFinances.Api.Services
{
    public class PacienteIntegrationHandler : IConsumer<PacienteEvent>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger _logger;

        public PacienteIntegrationHandler(IServiceProvider serviceProvider, ILogger<PacienteIntegrationHandler> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<PacienteEvent> context)
        {            
            await ProcessarPaciente(context.Message);
        }

        private async Task ProcessarPaciente(PacienteEvent message)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                try
                {
                    var commandHandler = scope.ServiceProvider.GetRequiredService<IMediator>();
                    var command = new PacienteCommand(message.Nome, message.Cpf, message.Cep, message.Endereco, message.Estado, message.Telefone, message.Ativo);
                    await commandHandler.Send(command);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "PacienteIntegrationHandler: {Nome}, CPF: {CPF}", message.Nome, message.Cpf);

                }
            }
        }
    }
}
