using MassTransit;
using AloDoutor.Core.Messages.Integration;

namespace AloFinances.Api.Services
{
    public class MedicoIntegrationHandler : IConsumer<MedicoEvent>
    {
        public Task Consume(ConsumeContext<MedicoEvent> context)
        {
            throw new NotImplementedException();
        }
    }
}
