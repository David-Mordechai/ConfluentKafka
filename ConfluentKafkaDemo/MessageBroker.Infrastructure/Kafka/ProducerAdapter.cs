using Confluent.Kafka;
using MessageBroker.Core.Models;
using MessageBroker.Core.Services.Interfaces;
using MessageBroker.Infrastructure.Kafka.Builder;

namespace MessageBroker.Infrastructure.Kafka;

internal class ProducerAdapter : IProducerAdapter
{
    private readonly IProducer<Null, string> _producer;
    public ProducerAdapter(ProducerBuilderAdapter producerBuilder)
    {
        _producer = producerBuilder.Build();
    }

    public async Task<DeliveryResultModel?> ProduceAsync(string topic, MessageModel message)
    {
        try
        {
            var dr = await _producer.ProduceAsync(topic, new Message<Null, string> { Value = message.Value });
            return new DeliveryResultModel(
                Message: dr.Message.Value,
                TopicPartitionOffset:
                $"{dr.TopicPartitionOffset.Topic} [{dr.TopicPartitionOffset.Partition}] @{dr.TopicPartitionOffset.Offset}");
        }
        catch (Exception e)
        {
            throw e switch
            {
                ProduceException<Null, string> exception => new Exception(exception.Error.Reason),
                _ => new Exception(e.Message)
            };
        }
    }

    public void Dispose()
    {
        _producer.Dispose();
        GC.SuppressFinalize(this);
    }
}