namespace CreditoApi.Domain.Creditos;

public interface ICreditoRepository
{
    Task AddAsync(Credito credito, CancellationToken cancellationToken = default);

    Task<Credito?> GetByNumeroCreditoAsync(string numeroCredito, CancellationToken cancellationToken = default);

    Task<IEnumerable<Credito>> GetByNumeroNfseAsync(string numeroNfse, CancellationToken cancellationToken = default);

    Task BulkInsertAsync(IEnumerable<Credito> creditos, CancellationToken cancellationToken = default);

    Task<bool> ExistsByNumeroCreditoAsync(string numeroCredito, CancellationToken cancellationToken = default);
}
