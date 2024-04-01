using AloDoutor.Api.Configuration;
using AloDoutor.Core.Identidade;
using AloDoutor.Infra.Ioc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddSerilogConfiguration(builder.Configuration, builder.Environment);

builder.Services.AddSwaggerConfiguration();

builder.Services.AddApiConfig(builder.Configuration);

builder.Services.AddJwtConfiguration(builder.Configuration);

builder.Services.AddMessageBusConfiguration(builder.Configuration);

builder.Services.RegisterServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwaggerConfiguration();

app.UseApiConfiguration(app.Environment);

app.Run();
