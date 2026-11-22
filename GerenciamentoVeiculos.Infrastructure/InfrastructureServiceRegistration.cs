using GerenciamentoVeiculos.Domain.Interfaces.Messaging;
using GerenciamentoVeiculos.Domain.Interfaces.Repositories;
using GerenciamentoVeiculos.Infrastructure.Data;
using GerenciamentoVeiculos.Infrastructure.Messaging;
using GerenciamentoVeiculos.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GerenciamentoVeiculos.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Configuração do EF Core com PostgreSQL
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        // Registro dos Repositórios
        services.AddScoped<IProprietarioRepository, ProprietarioRepository>();
        services.AddScoped<IVeiculoRepository, VeiculoRepository>();

        // Configuração da mensageria Azure Service Bus
        services.Configure<AzureServiceBusConfig>(configuration.GetSection(AzureServiceBusConfig.SectionName));
        services.AddSingleton<IFilaMensageriaService, AzureServiceBusFilaService>();

        return services;
    }
}
