using CreditoApi.Domain.Creditos;
using Microsoft.EntityFrameworkCore;

namespace CreditoApi.Infrastructure.Database.Repositories;

internal sealed class CreditoRepository : ICreditoRepository
{
    private readonly AppDbContext _context;

    public CreditoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Credito credito, CancellationToken cancellationToken = default)
    {
        await _context.Creditos.AddAsync(credito, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task BulkInsertAsync(IEnumerable<Credito> creditos, CancellationToken cancellationToken = default)
    {
        var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            foreach (Credito credito in creditos)
            {
                _context.Creditos.Add(credito);
            }
            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception)
        {
            transaction.Rollback();
            throw;
        }
        finally
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task<bool> ExistsByNumeroCreditoAsync(string numeroCredito, CancellationToken cancellationToken = default)
    {
        return await _context.Creditos.AsNoTracking()
            .AnyAsync(c => c.NumeroCredito == numeroCredito, cancellationToken);
    }

    public async Task<Credito?> GetByNumeroCreditoAsync(string numeroCredito, CancellationToken cancellationToken = default)
    {
        return await _context.Creditos.AsNoTracking()
            .FirstOrDefaultAsync(c => c.NumeroCredito == numeroCredito, cancellationToken);
    }

    public async Task<IEnumerable<Credito>> GetByNumeroNfseAsync(string numeroNfse, CancellationToken cancellationToken = default)
    {
        List<Credito> creditos = await _context.Creditos.AsNoTracking()
            .Where(c => c.NumeroNfse == numeroNfse)
            .ToListAsync(cancellationToken);

        return creditos;
    }
}
