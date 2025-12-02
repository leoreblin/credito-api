using Confluent.Kafka;
using CreditoApi.Application.Abstractions;
using CreditoApi.Domain.Creditos;
using CreditoApi.SharedKernel.Results;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SharedError = CreditoApi.SharedKernel.Errors.Error;

namespace CreditoApi.Application.Modules.Creditos;

internal sealed class CreditoService : ICreditoService
{
    private readonly IKafkaClientFactory _kafkaClientFactory;
    private readonly ICreditoRepository _creditoRepository;
    private readonly ILogger<CreditoService> _logger;
    private readonly string _entryTopic;

    public CreditoService(
        IKafkaClientFactory kafkaClientFactory,
        ICreditoRepository creditoRepository,
        IConfiguration configuration,
        ILogger<CreditoService> logger)
    {
        _kafkaClientFactory = kafkaClientFactory;
        _creditoRepository = creditoRepository;
        _logger = logger;
        _entryTopic = configuration["Kafka:Creditos:EntryTopic"] ?? "integrar-credito-constituido-entry";
    }

    public async Task<Result> IntegrarCreditosAsync(
        IEnumerable<Credito> creditos,
        CancellationToken cancellationToken)
    {
        List<Credito> creditoList = creditos.ToList();

        using IProducer<Null, Credito> producer = _kafkaClientFactory.CreateProducer<Credito>();

        foreach (Credito credito in creditoList)
        {
            if (await _creditoRepository.ExistsByNumeroCreditoAsync(credito.NumeroCredito, cancellationToken))
            {
                return Result.Failure(SharedError.Conflict(
                    "Credito.JaExiste",
                    $"O crédito {credito.NumeroCredito} já foi integrado anteriormente."));
            }

            await producer.ProduceAsync(
                _entryTopic,
                new Message<Null, Credito> { Value = credito },
                cancellationToken);

            _logger.LogInformation("Mensagem para crédito {NumeroCredito} publicada em {Topic}", credito.NumeroCredito, _entryTopic);
        }

        producer.Flush(TimeSpan.FromSeconds(5));

        return Result.Success();
    }

    public async Task<Result<Credito>> ObterPorNumeroCreditoAsync(
        string numeroCredito,
        CancellationToken cancellationToken = default)
    {
        Credito? credito = await _creditoRepository.GetByNumeroCreditoAsync(numeroCredito, cancellationToken);

        if (credito is null)
        {
            return Result.Failure<Credito>(
                SharedError.NotFound("Credito.NaoEncontrado", $"Crédito {numeroCredito} não encontrado."));
        }

        return Result.Success(credito);
    }

    public async Task<Result<IEnumerable<Credito>>> ObterPorNumeroNfseAsync(
        string numeroNfse,
        CancellationToken cancellationToken = default)
    {
        IEnumerable<Credito> creditos = await _creditoRepository.GetByNumeroNfseAsync(numeroNfse, cancellationToken);
        return Result.Success(creditos);
    }
}
