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
            services.AddScoped<IRequestHandler< PacienteCommand, ValidationResult>, FinancasCommandHandler>();
            services.AddScoped<IRequestHandler<PacienteRemovidoComand, ValidationResult>, FinancasCommandHandler>();
            services.AddScoped<IRequestHandler<MedicoCommand, ValidationResult>, FinancasCommandHandler>();
            services.AddScoped<IRequestHandler<MedicoRemovidoComand, ValidationResult>, FinancasCommandHandler>();
            services.AddScoped<IRequestHandler<ContaCommand, ValidationResult>, FinancasCommandHandler>();
            services.AddScoped<IRequestHandler<ContaCanceladaComand, ValidationResult>, FinancasCommandHandler>();

            //Repository
            services.AddScoped<AloFinancesContext>();
            services.AddScoped<IPacienteRepository, PacienteRepository>();
            services.AddScoped<IMedicoRepository, MedicoRepository>();
            services.AddScoped<IPrecoRepository, PrecoRepository>();
            services.AddScoped<IContaRepository, ContasRepository>();
        }


    }
}
