using HealthMedScheduler.Api.Middlewares;
using HealthMedScheduler.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Extensions.Logging;
using Serilog.Formatting.Compact;
using System.Reflection;

namespace HealthMedScheduler.Api
{
    public static class ApiServiceRegistration
    {
        public static IServiceCollection AddApiConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MeuDbContext>(options =>
               options.UseSqlServer(Environment.GetEnvironmentVariable("ConnectionString")));


            services.AddControllers();

            services.AddHttpContextAccessor();

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
            app.UseDeveloperExceptionPage();

            if (env.IsDevelopment() || env.IsStaging())
            {               

                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var dbContextAuth = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                    dbContextAuth.Database.Migrate();

                    var dbContext = scope.ServiceProvider.GetRequiredService<MeuDbContext>();
                     dbContext.Database.Migrate();
                }
            }

          

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors("Total");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        
        public static void AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo()
                {

                    Title = "Health&Med API",
                    Description = "Esta API é Controle de Agendamentos de Consulta",
                    Contact = new OpenApiContact() { Name = "Health&Med", Email = "postechdotnet@gmail.com " }
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Insira o token JWT desta maneira: Bearer {seu token}",
                    Name = "Authorization",
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });
        }

        public static void UseSwaggerConfiguration(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });
        }

        public static void AddSerilogConfiguration(this IServiceCollection services, IConfiguration configuration,
           IHostEnvironment environment)
        {
            var logConfig = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .Enrich.WithProperty("Application", "Health&Med")
                .ReadFrom.Configuration(configuration)                
                .WriteTo.Console(new CompactJsonFormatter())
                .CreateLogger();

            ILoggerProvider serilogProvider = new SerilogLoggerProvider(logConfig);

            services.AddSingleton<ILoggerProvider>(serilogProvider);
            services.AddSingleton<ILoggerFactory, LoggerFactory>(serviceProvider =>
            {
                var factory = new LoggerFactory();
                factory.AddProvider(serviceProvider.GetRequiredService<ILoggerProvider>());
                return factory;
            });
        }

        private static string GetLogFilePath(IHostEnvironment environment)
        {
            var folderPath = Path.Combine(environment.ContentRootPath, "Logs");

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            return Path.Combine(folderPath, "log.txt");
        }
    }
}
