using AloDoutor.Core.Identidade;
using AloFinances.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace AloFinances.Api.Configuration
{
    public static class ApiConfig
    {
        public static IServiceCollection AddApiConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AloFinancesContext>(options =>
               options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddControllers();

            services.AddCors(options =>
            {
                options.AddPolicy("Total",
                    builder =>
                        builder
                            .AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader());
            });

            return services;
        }

        public static void UseApiConfiguration(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment() || env.IsStaging())
            {
                app.UseDeveloperExceptionPage();

                /* using (var scope = app.ApplicationServices.CreateScope())
                 {
                     var dbContext = scope.ServiceProvider.GetRequiredService<MeuDbContext>();
                     dbContext.Database.Migrate();
                 }*/
            }


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthConfiguration();

            app.UseCors("Total");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
