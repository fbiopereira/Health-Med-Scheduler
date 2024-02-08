using AloDoutor.Core.Messages.Integration;
using AloFinances.Api.Application.Commands;
using MassTransit;
using MediatR;

namespace AloFinances.Api.Services
{
    public class PacienteIntegrationHandler : IConsumer<PacienteEvent>
    {
        private readonly IServiceProvider _serviceProvider;

        public PacienteIntegrationHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task Consume(ConsumeContext<PacienteEvent> context)
        {
            await ProcessarConta(context.Message);
        }

        private async Task ProcessarConta(PacienteEvent message)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var commandHandler = scope.ServiceProvider.GetRequiredService<IMediator>();
                var command = new PacienteComand(message.Nome, message.Cpf, message.Cep, message.Endereco, message.Estado, message.Telefone);
                await commandHandler.Send(command);
            }
        }
    }
}
