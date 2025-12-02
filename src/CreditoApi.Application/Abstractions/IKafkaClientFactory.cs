using Confluent.Kafka;

namespace CreditoApi.Application.Abstractions;

public interface IKafkaClientFactory
{
    IProducer<Null, TEntity> CreateProducer<TEntity>();
    IConsumer<Null, TEntity> CreateConsumer<TEntity>();
}
