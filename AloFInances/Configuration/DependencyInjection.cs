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
            services.AddScoped<IRequestHandler<MedicoComand, ValidationResult>, FinancasCommandHandler>();
            services.AddScoped<IRequestHandler<MedicoRemovidoComand, ValidationResult>, FinancasCommandHandler>();

            //Repository
            services.AddScoped<AloFinancesContext>();
            services.AddScoped<IPacienteRepository, PacienteRepository>();
            services.AddScoped<IMedicoRepository, MedicoRepository>();
        }


    }
}
