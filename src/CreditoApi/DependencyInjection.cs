using Confluent.Kafka;
using CreditoApi.Infrastructure;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace CreditoApi;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddControllers();
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();
        services.ConfigureHealthChecks(configuration);

        return services;
    }

    private static IServiceCollection ConfigureHealthChecks(this IServiceCollection services, IConfiguration configuration)
    {
        string bootstrapServers = configuration["Kafka:BootstrapServers"] ?? "localhost:9092";

        services.AddHealthChecks()
            .AddCheck("liveness", () => HealthCheckResult.Healthy("Processo Liveness OK."), tags: ["self"])
            .AddKafka(new ProducerConfig { BootstrapServers = bootstrapServers },
                name: "Kafka Check", tags: ["ready"]);

        return services;
    }
}
