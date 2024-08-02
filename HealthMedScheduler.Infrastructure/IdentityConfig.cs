using HealthMedScheduler.Application.Extensions;
using HealthMedScheduler.Infrastructure.Data.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;
using Health.Core.Identidade;

namespace HealthMedScheduler.Infrastructure
{
    public static class IdentityConfig
    {
        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Environment.GetEnvironmentVariable("ConnectionString")));

            services.AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddErrorDescriber<IdentityMensagensPortugues>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();


            services.AddJwtConfiguration(configuration);

            return services;
        }

        public static async Task CreateUserDefault(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            // Verifique se o usuário já existe
            var user = await userManager.FindByNameAsync("postechdotnet@gmail.com");

            if (user == null)
            {
                user = new IdentityUser
                {
                    UserName = "postechdotnet@gmail.com",
                    Email = "postechdotnet@gmail.com",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(user, "Pos@123");

                //Se o usuário foi criado, vai ser adicionado o Nivel de acesso como administrador
                if (result.Succeeded)
                {

                    var claim = new Claim("Administrador", "Cadastrar");

                    await userManager.AddClaimAsync(user, claim);
                }
            }
        }
    }
}