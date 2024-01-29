using AloDoutor.Core.Usuario;
using AloDoutor.Domain.Interfaces;
using AloDoutor.Domain.Services;
using AloDoutor.Infra.Data.Context;
using AloDoutor.Infra.Data.Repository;

namespace AloDoutor.Api.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            //Autenticação
            services.AddScoped<IAspNetUser, AspNetUser>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

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


            return services;
        }
    }
}
