using ConfluentKafkaDemo.Application.MessageBroker.Record;

namespace ConfluentKafkaDemo.Application.MessageBroker;

public interface IProducerAdapter : IDisposable
{
    Task<DeliveryResultRecord> ProduceAsync(string topic, string message);
}