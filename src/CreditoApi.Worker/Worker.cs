using Confluent.Kafka;
using CreditoApi.Application.Abstractions;
using CreditoApi.Domain.Creditos;

namespace CreditoApi.Worker;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IKafkaClientFactory _kafkaClientFactory;
    private readonly ICreditoRepository _creditoRepository;
    private readonly string _entryTopic;

    public Worker(
        ILogger<Worker> logger,
        IKafkaClientFactory kafkaClientFactory,
        ICreditoRepository creditoRepository,
        IConfiguration configuration)
    {
        _logger = logger;
        _kafkaClientFactory = kafkaClientFactory;
        _creditoRepository = creditoRepository;
        _entryTopic = configuration["Kafka:Creditos:EntryTopic"] ?? "integrar-credito-constituido-entry";
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using IConsumer<Null, Credito> consumer = _kafkaClientFactory.CreateConsumer<Credito>();
        consumer.Subscribe(_entryTopic);

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                ConsumeResult<Null, Credito>? result = consumer.Consume(TimeSpan.FromMilliseconds(100));

                if (result?.Message?.Value is { } credito)
                {
                    await _creditoRepository.AddAsync(credito, stoppingToken);
                    consumer.Commit(result);
                    _logger.LogInformation("Crédito {NumeroCredito} inserido pelo worker.", credito.NumeroCredito);
                }
            }
            catch (ConsumeException ex)
            {
                _logger.LogError(ex, "Erro ao consumir mensagem do Kafka.");
            }
            catch (Exception ex) when (ex is not OperationCanceledException)
            {
                _logger.LogError(ex, "Erro ao processar crédito recebido do tópico.");
            }

            await Task.Delay(TimeSpan.FromMilliseconds(500), stoppingToken);
        }

        consumer.Close();
    }
}
