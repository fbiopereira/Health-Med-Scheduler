using AloFinances.Api.Application.Commands;
using MediatR;

namespace AloFinances.Api.Configuration
{
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IRequestHandler< PacienteComand, bool>, FinancasCommandHanlder>();
        }


    }
}
