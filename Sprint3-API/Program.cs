using System.Text.Json;
using System.Threading.RateLimiting;
using Asp.Versioning;
using DotNetEnv;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using Sprint3_API;
using Sprint3_API.Endpoints;
using Sprint3_API.Services;

using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// Configuração do Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API Mottu Mottion",
        Version = "v1",
        Description = "API .NET para gerenciamento dos pátios da Mottu."
    });

    options.ExampleFilters();
    options.IgnoreObsoleteActions();
    options.IgnoreObsoleteProperties();
});

builder.Services.AddSwaggerExamplesFromAssemblyOf<Program>();

Env.Load();

// Conexão com o banco de dados
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseOracle(Environment.GetEnvironmentVariable("ConnectionStrings__OracleConnection")));

// Escopos das Services
builder.Services.AddScoped<ClienteService>();
builder.Services.AddScoped<MotoService>();
builder.Services.AddScoped<PatioService>();
builder.Services.AddScoped<CargoService>();
builder.Services.AddScoped<FuncionarioService>();
builder.Services.AddScoped<GerenteService>();
builder.Services.AddScoped<VagaService>();
builder.Services.AddScoped<SetorService>();
builder.Services.AddScoped<MovimentacaoService>();

// Define o limite de requisições
builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("fixed", opt =>
    {
        opt.PermitLimit = 5;
        opt.Window = TimeSpan.FromSeconds(10);
        opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        opt.QueueLimit = 2;
    });
});

// Configurações de CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(opt =>
    {
        opt.AllowAnyOrigin();
        opt.AllowAnyMethod();
        opt.AllowAnyHeader();
        opt.WithExposedHeaders("Content-Type", "Accept");
    });
});

// Configurações JSON
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});

// Configuração de SignalR
builder.Services.AddSignalR();

var app = builder.Build();

// Habilita o uso do CORS
app.UseCors();

// Limita a quantidade de requisições
app.UseRateLimiter();

// Configuração do Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "API Mottu Mottion v1");
    });
}

// Versionamento de API
var apiVersionSet = app.NewApiVersionSet()
    .HasApiVersion(new ApiVersion(1))
    .Build();

// Endpoints
app.MapClienteEndpoints();
app.MapMotoEndpoints();
app.MapPatioEndpoints();
app.MapCargoEndpoints();
app.MapFuncionarioEndpoints();
app.MapGerenteEndpoints();
app.MapVagaEndpoints();
app.MapSetorEndpoints();
app.MapMovimentacaoEndpoints();

await app.RunAsync();