using MassTransit;
using AloDoutor.Core.Messages.Integration;
using AloFinances.Api.Application.Commands;
using MediatR;

namespace AloFinances.Api.Services
{
    public class MedicoIntegrationHandler : IConsumer<MedicoEvent>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger _logger;

        public MedicoIntegrationHandler(IServiceProvider serviceProvider, ILogger<MedicoIntegrationHandler> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<MedicoEvent> context)
        {
            await ProcessarMedico(context.Message);
        }

        private async Task ProcessarMedico(MedicoEvent message)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                try
                {
                    var commandHandler = scope.ServiceProvider.GetRequiredService<IMediator>();
                    var command = new MedicoCommand(message.Nome, message.Cpf, message.Cep, message.Endereco, message.Estado, message.Telefone, message.Ativo, message.Crm);
                    await commandHandler.Send(command);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "MedicoIntegrationHandler: {Nome}, CPF: {CPF}, CRM: {crm}", message.Nome, message.Cpf, message.Crm);

                }
            }
        }
    }
}
