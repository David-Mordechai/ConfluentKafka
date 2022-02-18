using ConfluentKafkaDemo.Core.MessageBroker.Record;

namespace ConfluentKafkaDemo.Core.MessageBroker;

public interface IConsumerAdapter : IDisposable
{
    void Subscribe(string topic);
    ConsumeResultRecord Consume(CancellationToken cancellationToken);
}