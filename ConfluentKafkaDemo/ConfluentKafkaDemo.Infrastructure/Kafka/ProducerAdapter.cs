﻿using Confluent.Kafka;
using ConfluentKafkaDemo.Core.MessageBroker;
using ConfluentKafkaDemo.Core.MessageBroker.Record;

namespace ConfluentKafkaDemo.Infrastructure.Kafka;

internal class ProducerAdapter : IProducerAdapter
{
    private readonly IProducer<Null, string> _producer;
    public ProducerAdapter(IProducer<Null, string> producer)
    {
        _producer = producer;
    }

    public async Task<DeliveryResultRecord> ProduceAsync(string topic, string message)
    {
        try
        {
            var dr = await _producer.ProduceAsync("testTopic", new Message<Null, string> { Value = message });
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