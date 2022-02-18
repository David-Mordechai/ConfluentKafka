using Confluent.Kafka;
using ConfluentKafkaDemo.Application.MessageBroker;
using ConfluentKafkaDemo.Application.MessageBroker.Record;

namespace ConfluentKafkaDemo.Infrastructure.Kafka;

internal class ProducerAdapter : IProducerAdapter
{
    private readonly IProducer<Null, string> _producer;
    public ProducerAdapter(IProducer<Null, string> producer)
    {
        _producer = producer;
    }

    public async Task<DeliveryResultRecord> ProduceAsync(string topic, MessageRecord message)
    {
        try
        {
            var dr = await _producer.ProduceAsync(topic, new Message<Null, string> { Value = message.Value });
            return new DeliveryResultRecord(
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