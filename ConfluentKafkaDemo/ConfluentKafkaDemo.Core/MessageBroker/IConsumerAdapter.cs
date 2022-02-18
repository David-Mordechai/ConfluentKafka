using ConfluentKafkaDemo.Application.MessageBroker.Record;

namespace ConfluentKafkaDemo.Application.MessageBroker;

public interface IConsumerAdapter : IDisposable
{
    void Subscribe(string topic);
    ConsumeResultRecord Consume(CancellationToken cancellationToken);
}