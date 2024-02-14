using AloDoutor.Core.Messages;
using AloFinances.Api.Services;
using MassTransit;

namespace AloFinances.Api.Configuration
{
    public static class MessageBusConfigRabbit
    {
        public static void AddMessageBusConfigurationRabbit(this IServiceCollection services,
           IConfiguration configuration)
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumer<PagamentoRealizadoIntegrationHandler>();
                x.AddConsumer<PagamentoCanceladoIntegrationHandler>();
                x.AddConsumer<PacienteIntegrationHandler>();
                x.AddConsumer<PacienteRemoverIntegrationHandler>();
                x.AddConsumer<MedicoIntegrationHandler>();
                x.AddConsumer<MedicoRemoverIntegrationHandler>();
                x.SetKebabCaseEndpointNameFormatter();
                x.UsingRabbitMq((context, cfg) =>
                {
                    var rabbitMQConfig = configuration.GetSection("MessageQueueConnection:MassTransit");
                    cfg.Host(new Uri(rabbitMQConfig["host"] ?? ""), h =>
                    {
                        h.PublisherConfirmation = rabbitMQConfig.GetValue<bool>("publisherConfirms");
                        h.Username(rabbitMQConfig["credentials:username"]);
                        h.Password(rabbitMQConfig["credentials:password"]);
                    });

                    cfg.ConfigureEndpoints(context);                 
                });                
            });
        }
    }
}
