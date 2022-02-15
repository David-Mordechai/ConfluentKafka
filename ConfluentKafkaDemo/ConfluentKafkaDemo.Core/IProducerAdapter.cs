using ConfluentKafkaDemo.Core.Record;

namespace ConfluentKafkaDemo.Core
{
    public interface IProducerAdapter : IDisposable
    {
        Task<DeliveryResultRecord> ProduceAsync(string topic, string message);
    }
}
