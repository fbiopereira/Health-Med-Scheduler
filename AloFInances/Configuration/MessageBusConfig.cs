using AloFInances.Services;
using MassTransit;

namespace AloFinances.Configuration
{
    public static class MessageBusConfig
    {
        public static void AddMessageBusConfiguration(this IServiceCollection services,
           IConfiguration configuration)
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumer<PagamentoRealizadoIntegrationHandler>();
                x.AddConsumer<PagamentoCanceladoIntegrationHandler>();
                x.SetKebabCaseEndpointNameFormatter();
                x.UsingRabbitMq((context, cfg) =>
                {
                    var rabbitMQConfig = configuration.GetSection("MessageQueueConnection:MassTransit");
                    cfg.Host(new Uri(rabbitMQConfig["host"]), h =>
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
