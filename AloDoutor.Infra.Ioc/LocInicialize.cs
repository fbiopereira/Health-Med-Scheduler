using AloDoutor.Domain.Interfaces;
using AloDoutor.Domain.Services;
using AloDoutor.Infra.Data.Data.Context;
using AloDoutor.Infra.Data.Data.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace AloDoutor.Infra.Ioc
{
    public static class LocInicialize
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            //Repository
            services.AddScoped<MeuDbContext>();
            services.AddScoped<IEspecialidadeMedicoRepository, EspecialidadeMedicoRepository>();
            services.AddScoped<IEspecialidadeRepository, EspecialidadeRepository>();
            services.AddScoped<IMedicoRepository, MedicoRepository>();
            services.AddScoped<IPacienteRepository, PacienteRepository>();
            services.AddScoped<IAgendamentoRepository, AgendamentoRepository>();

            //Servivces
            services.AddScoped<IEspecialidadeService, EspecialidadeService>();
            services.AddScoped<IAgendamentoService, AgendamentoService>();
            services.AddScoped<IPacienteService, PacienteService>();
            services.AddScoped<IMedicoService, MedicoService>();
            services.AddScoped<IEspecialidadeMedicoService, EspecialidadeMedicoService>();
        }
    }
}
