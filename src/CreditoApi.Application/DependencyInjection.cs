using CreditoApi.Application.Modules.Creditos;
using CreditoApi.Domain.Creditos;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace CreditoApi.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ICreditoService, CreditoService>();
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly, includeInternalTypes: true);
        return services;
    }
}
