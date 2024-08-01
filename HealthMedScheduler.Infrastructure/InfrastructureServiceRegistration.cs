using HealthMedScheduler.Application.Interfaces.Email;
using HealthMedScheduler.Application.Models;
using HealthMedScheduler.Domain.Interfaces;
using HealthMedScheduler.Infrastructure.Data.Context;
using HealthMedScheduler.Infrastructure.Data.Repository;
using HealthMedScheduler.Infrastructure.EmailService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HealthMedScheduler.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            //Repository
            services.AddScoped<MeuDbContext>();
            services.AddScoped<IEspecialidadeMedicoRepository, EspecialidadeMedicoRepository>();
            services.AddScoped<IEspecialidadeRepository, EspecialidadeRepository>();
            services.AddScoped<IMedicoRepository, MedicoRepository>();
            services.AddScoped<IPacienteRepository, PacienteRepository>();
            services.AddScoped<IAgendamentoRepository, AgendamentoRepository>();

            //Services

            services.Configure<EmailSettings>(o => configuration.GetSection("EmailSettings").Bind(o));
            services.AddScoped<IEmailSender, EmailSender>();

            return services;
        }
    }
}
