using MassTransit;

namespace AloDoutor.Api.Configuration
{
    public static class MessageBusConfigcs
    {
        public static void AddMessageBusConfiguration(this IServiceCollection services,
                  IConfiguration configuration)
        {
            services.AddMassTransit(x =>
            {
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

                    cfg.ConfigureEndpoints(context, new KebabCaseEndpointNameFormatter("dev", false));
                });
            });
        }
    }
}
