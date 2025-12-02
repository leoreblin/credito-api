using CreditoApi.Domain.Creditos;
using Microsoft.EntityFrameworkCore;

namespace CreditoApi.Infrastructure.Database.Seed;

public static class DbSeeder
{
    public static async Task SeedAsync(AppDbContext context, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(context);

        bool alreadySeeded = await context.Creditos.AsNoTracking().AnyAsync(cancellationToken);
        if (alreadySeeded)
        {
            return;
        }

        Credito[] creditos =
        [
            new()
            {
                NumeroCredito = "123456",
                NumeroNfse = "7891011",
                DataConstituicao = new DateOnly(2024, 2, 25),
                ValorIssqn = 1500.75m,
                TipoCredito = "ISSQN",
                SimplesNacional = true,
                Aliquota = 5.0m,
                ValorFaturado = 30000.00m,
                ValorDeducao = 5000.00m,
                BaseCalculo = 25000.00m
            },
            new()
            {
                NumeroCredito = "789012",
                NumeroNfse = "7891011",
                DataConstituicao = new DateOnly(2024, 2, 26),
                ValorIssqn = 1200.50m,
                TipoCredito = "ISSQN",
                SimplesNacional = false,
                Aliquota = 4.5m,
                ValorFaturado = 25000.00m,
                ValorDeducao = 4000.00m,
                BaseCalculo = 21000.00m
            },
            new()
            {
                NumeroCredito = "654321",
                NumeroNfse = "1122334",
                DataConstituicao = new DateOnly(2024, 1, 15),
                ValorIssqn = 800.50m,
                TipoCredito = "Outros",
                SimplesNacional = true,
                Aliquota = 3.5m,
                ValorFaturado = 20000.00m,
                ValorDeducao = 3000.00m,
                BaseCalculo = 17000.00m
            }
        ];

        await context.Creditos.AddRangeAsync(creditos, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}
