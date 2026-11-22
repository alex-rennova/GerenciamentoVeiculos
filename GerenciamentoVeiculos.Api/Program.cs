using System.Reflection;
using GerenciamentoVeiculos.Api.Middlewares;
using Microsoft.EntityFrameworkCore;
using GerenciamentoVeiculos.Api.Workers;
using GerenciamentoVeiculos.Application;
using GerenciamentoVeiculos.Infrastructure;
using GerenciamentoVeiculos.Infrastructure.Data;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// 1. Configuração dos Controllers e conversão de rotas em minúsculas
builder.Services.AddControllers();
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// 2. Configuração de CORS para permitir que o cliente Blazor consuma a API livremente
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTudo", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// 3. Injeção de Dependência das camadas de Application e Infrastructure
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

// 4. Registro do Background Worker para consumo contínuo da Fila da Azure
builder.Services.AddHostedService<ProcessadorFilaWorker>();

// 5. Configuração do Swagger com documentação XML rica
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API de Gerenciamento de Veículos e Proprietários",
        Version = "v1",
        Description = "API REST desenvolvida em .NET 8 com Clean Architecture, Entity Framework Core (In-Memory), FluentValidation e Mensageria com Azure Service Bus (com Background Worker).",
        Contact = new OpenApiContact
        {
            Name = "Candidato Sênior .NET",
            Url = new Uri("https://github.com")
        }
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }
});

var app = builder.Build();

// 6. Aplica migrations e seed dos dados no PostgreSQL
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await dbContext.Database.MigrateAsync();
    await DbInitializer.InicializarDadosAsync(dbContext);
}

// 7. Pipeline HTTP: Middleware global de tratamento de exceções (RFC 7807)
app.UseMiddleware<TratamentoExcecaoMiddleware>();

// 8. Swagger disponível sempre (ideal para avaliação em ambiente local e produção)
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Gerenciamento de Veículos v1");
    c.RoutePrefix = string.Empty; // Inicia o Swagger direto na raiz (http://localhost:porta/)
});

// Habilita a política de CORS antes do roteamento dos controllers
app.UseCors("PermitirTudo");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
