using ConfluentKafkaDemo.Core.Record;

namespace ConfluentKafkaDemo.Core
{
    public interface IConsumerAdapter : IDisposable
    {
        void Subscribe(string topic);
        ConsumeResultRecord Consume(CancellationToken cancellationToken);
    }
}
