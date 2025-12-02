using CreditoApi.SharedKernel.Results;

namespace CreditoApi.Domain.Creditos;

public interface ICreditoService
{
    Task<Result> IntegrarCreditosAsync(IEnumerable<Credito> creditos, CancellationToken cancellationToken = default);

    Task<Result<IEnumerable<Credito>>> ObterPorNumeroNfseAsync(string numeroNfse, CancellationToken cancellationToken = default);

    Task<Result<Credito>> ObterPorNumeroCreditoAsync(string numeroCredito, CancellationToken cancellationToken = default);
}
