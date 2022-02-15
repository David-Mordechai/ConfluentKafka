using Confluent.Kafka;
using ConfluentKafkaDemo.Core;
using ConfluentKafkaDemo.Core.Record;

namespace ConfluentKafkaDemo.Infrastructure
{
    internal class ConsumerAdapter : IConsumerAdapter
    {
        private readonly IConsumer<Ignore, string> _consumer;

        public ConsumerAdapter(IConsumer<Ignore, string> consumer)
        {
            _consumer = consumer;
        }

        public void Subscribe(string topic)
        {
            _consumer.Subscribe(topic);
        }

        public ConsumeResultRecord Consume(CancellationToken cancellationToken)
        {
            try
            {
                var consumeResult = _consumer.Consume(cancellationToken);
                return new ConsumeResultRecord(
                    Message: consumeResult.Message.Value,
                    TopicPartitionOffset:
                    $"{consumeResult.TopicPartitionOffset.Topic} [{consumeResult.TopicPartitionOffset.Partition}] @{consumeResult.TopicPartitionOffset.Offset}");
            }
            catch (Exception ex)
            {
                switch (ex)
                {
                    case OperationCanceledException:
                        // Ensure the consumer leaves the group cleanly and final offsets are committed.
                        _consumer.Close();
                        throw new OperationCanceledException("Operation was canceled.");
                    case ConsumeException exception:
                        throw new Exception(exception.Error.Reason);
                    default:
                        throw new Exception(ex.Message);
                }
            }
        }

        public void Dispose()
        {
            _consumer.Dispose();
        }
    }
}
