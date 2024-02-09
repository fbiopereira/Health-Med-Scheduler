using AloDoutor.Core.Messages.Integration;
using AloFinances.Api.Services;
using MassTransit;

namespace AloFinances.Api.Configuration
{
    public static class MessageBusConfigAzure
    {
        public static void AddMessageBusConfigurationAzure(this IServiceCollection services,
           IConfiguration configuration)
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumer<PagamentoRealizadoIntegrationHandler>();
                x.AddConsumer<PagamentoCanceladoIntegrationHandler>();

                x.SetKebabCaseEndpointNameFormatter();
                x.UsingAzureServiceBus((context, cfg) =>
                {
                    var azureMQConfig = configuration.GetSection("MessageQueueConnection:MassTransitAzure");
                    cfg.Host(azureMQConfig["host"]);
                    var topico = azureMQConfig["topico"] ?? string.Empty;
                    cfg.Message<AgendamentoCanceladoEvent>(t =>
                    {
                        t.SetEntityName(azureMQConfig["topico"] ?? string.Empty);
                    });
                });
            });
        }
    }
}
