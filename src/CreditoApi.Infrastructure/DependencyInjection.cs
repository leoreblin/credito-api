using CreditoApi.Application.Abstractions;
using CreditoApi.Domain.Creditos;
using CreditoApi.Infrastructure.Database;
using CreditoApi.Infrastructure.Database.Repositories;
using CreditoApi.Infrastructure.Messaging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CreditoApi.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabase(configuration);
        services.AddHealthChecks(configuration);
        services.AddScoped<ICreditoRepository, CreditoRepository>();
        services.AddSingleton<IKafkaClientFactory, KafkaClientFactory>();
        return services;
    }

    private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("CreditoApiDatabase");

        services.AddDbContext<AppDbContext>(
            options => options
                .UseNpgsql(connectionString, npgsqlOptions =>
                    npgsqlOptions.MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.CreditoApi))
                .UseSnakeCaseNamingConvention());

        services.AddScoped<IAppDbContext>(provider => provider.GetRequiredService<AppDbContext>());

        return services;
    }

    private static IServiceCollection AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddHealthChecks()
            .AddNpgSql(
                configuration.GetConnectionString("CreditoApiDatabase")!,
                name: "CreditoApi Database Check",
                tags: ["ready"]);

        return services;
    }
}
