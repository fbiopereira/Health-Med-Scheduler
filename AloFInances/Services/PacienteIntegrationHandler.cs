using AloDoutor.Core.Messages;
using MassTransit;

namespace AloFinances.Api.Services
{
    public class PacienteIntegrationHandler : IConsumer<PacienteEvent>
    {
        public Task Consume(ConsumeContext<PacienteEvent> context)
        {
            throw new NotImplementedException();
        }
    }
}
