using System.Text.Json;
using System.Text.Json.Serialization;
using Confluent.Kafka;
using CreditoApi.Application.Abstractions;
using Microsoft.Extensions.Configuration;

namespace CreditoApi.Infrastructure.Messaging;

public sealed class KafkaClientFactory : IKafkaClientFactory
{
    private readonly IConfiguration _configuration;
    private readonly ConsumerConfig _consumerConfig;
    private readonly ProducerConfig _producerConfig;

    public KafkaClientFactory(IConfiguration configuration)
    {
        _configuration = configuration;
        string bootstrapServers = _configuration["Kafka:BootstrapServers"] ?? "localhost:9092";
        string groupId = _configuration["Kafka:ConsumerGroupId"] ?? "credito-api-group";

        _consumerConfig = new ConsumerConfig
        {
            BootstrapServers = bootstrapServers,
            GroupId = groupId,
            AutoOffsetReset = AutoOffsetReset.Earliest,
        };

        _producerConfig = new ProducerConfig
        {
            BootstrapServers = bootstrapServers,
        };
    }

    public IConsumer<Null, TEntity> CreateConsumer<TEntity>()
    {
        return new ConsumerBuilder<Null, TEntity>(_consumerConfig)
            .SetValueDeserializer(new JsonDeserializer<TEntity>())
            .Build();
    }

    public IProducer<Null, TEntity> CreateProducer<TEntity>()
    {
        return new ProducerBuilder<Null, TEntity>(_producerConfig)
            .SetValueSerializer(new JsonSerializer<TEntity>())
            .Build();
    }

    private sealed class JsonSerializer<TEntity> : ISerializer<TEntity>
    {
        private static readonly System.Text.Json.JsonSerializerOptions SerializerOptions = new()
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public byte[] Serialize(TEntity data, SerializationContext context)
        {
            return System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(data, SerializerOptions);
        }
    }

    private sealed class JsonDeserializer<TEntity> : IDeserializer<TEntity>
    {
        private static readonly System.Text.Json.JsonSerializerOptions SerializerOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public TEntity Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
        {
            if (isNull || data.IsEmpty)
            {
                return default!;
            }

            return System.Text.Json.JsonSerializer.Deserialize<TEntity>(data, SerializerOptions)!;
        }
    }
}
