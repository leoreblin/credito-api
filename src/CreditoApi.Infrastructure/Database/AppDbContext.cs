using CreditoApi.Application.Abstractions;
using CreditoApi.Domain.Creditos;
using Microsoft.EntityFrameworkCore;

namespace CreditoApi.Infrastructure.Database;

public sealed class AppDbContext(
    DbContextOptions<AppDbContext> options) : DbContext(options), IAppDbContext
{
    public DbSet<Credito> Creditos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        modelBuilder.HasDefaultSchema("credito_api");
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }
}
