using FluentValidation;
using GerenciamentoVeiculos.Application.Interfaces;
using GerenciamentoVeiculos.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GerenciamentoVeiculos.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Registro automático de todos os validadores do FluentValidation neste assembly
        services.AddValidatorsFromAssembly(typeof(ApplicationServiceRegistration).Assembly);

        // Registro dos serviços de aplicação
        services.AddScoped<IProprietarioService, ProprietarioService>();
        services.AddScoped<IVeiculoService, VeiculoService>();

        return services;
    }
}
