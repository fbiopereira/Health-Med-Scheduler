using MassTransit;
using Relatorio.API.Services;

namespace Relatorio.API.Configuration
{
    public static class MessageBusConfigAzure
    {
        public static void AddMessageBusConfigurationAzure(this IServiceCollection services,
           IConfiguration configuration)
        {
            services.AddMassTransit(x =>
            {  
                x.SetKebabCaseEndpointNameFormatter();
                x.UsingAzureServiceBus((context, cfg) =>
                {
                    var azureMQConfig = configuration.GetSection("MessageQueueConnection:MassTransitAzure");
                    cfg.Host(azureMQConfig["host"]);
                    cfg.SubscriptionEndpoint("sub-1", azureMQConfig["topico"] ?? string.Empty, c =>
                    {
                        c.Consumer<AgendamentoIntegratorHanlder>();
                    });

                   /* cfg.ReceiveEndpoint("fila", e =>
                    {
                        e.Consumer<AgendamentoIntegratorHanlder>();
                    });*/
                });
            });
        }
    }
}
