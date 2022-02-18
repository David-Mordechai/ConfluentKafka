using ConfluentKafkaDemo.Core.MessageBroker.Record;

namespace ConfluentKafkaDemo.Core.MessageBroker;

public interface IProducerAdapter : IDisposable
{
    Task<DeliveryResultRecord> ProduceAsync(string topic, string message);
}