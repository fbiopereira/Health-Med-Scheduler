using Identidade.API.Data;
using Identidade.API.Extensions;
using Microsoft.AspNetCore.Identity;
using AloDoutor.Core.Identidade;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Identidade.API.Configuration
{
    public static class IdentityConfig
    {
        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

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