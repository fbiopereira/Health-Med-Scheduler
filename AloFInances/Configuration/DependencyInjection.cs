using AloFinances.Api.Application.Commands;
using AloFinances.Domain.Interfaces;
using AloFinances.Infra.Context;
using AloFinances.Infra.Repository;
using FluentValidation.Results;
using MediatR;

namespace AloFinances.Api.Configuration
{
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IRequestHandler< PacienteComand, ValidationResult>, FinancasCommandHandler>();
            services.AddScoped<IRequestHandler<PacienteRemovidoComand, ValidationResult>, FinancasCommandHandler>();

            //Repository
            services.AddScoped<AloFinancesContext>();
            services.AddScoped<IPacienteRepository, PacienteRepository>();
        }


    }
}
