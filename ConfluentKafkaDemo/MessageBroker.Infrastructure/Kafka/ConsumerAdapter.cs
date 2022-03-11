using Confluent.Kafka;
using MessageBroker.Core.Models;
using MessageBroker.Core.Services.Interfaces;
using MessageBroker.Infrastructure.Kafka.Builder;

namespace MessageBroker.Infrastructure.Kafka;

internal class ConsumerAdapter : IConsumerAdapter
{
    private readonly IConsumer<Ignore, string> _consumer;

    public ConsumerAdapter(ConsumerBuilderAdapter consumerBuilder)
    {
        _consumer = consumerBuilder.Build();
    }

    public void Subscribe(string topic)
    {
        _consumer.Subscribe(topic);
    }

    public void Consume(CancellationToken cancellationToken, Action<ConsumeResultModel> consumeMessageHandler)
    {
        while (cancellationToken.IsCancellationRequested is false)
        {
            try
            {
                var consumeResult = _consumer.Consume(cancellationToken);
                var consumeResultModel = new ConsumeResultModel(
                    Message: consumeResult.Message.Value,
                    TopicPartitionOffset:
                    $"{consumeResult.TopicPartitionOffset.Topic} [{consumeResult.TopicPartitionOffset.Partition}] @{consumeResult.TopicPartitionOffset.Offset}");
                consumeMessageHandler.Invoke(consumeResultModel);
            }
            catch (Exception ex)
            {
                switch (ex)
                {
                    case OperationCanceledException:
                        // Ensure the consumer leaves the group cleanly and final offsets are committed.
                        _consumer.Close();
                        throw new Exception("Operation was canceled.");
                    case ConsumeException exception:
                        throw new Exception(exception.Error.Reason);
                    default:
                        throw new Exception(ex.Message);
                }
            }
        }
    }

    public void Dispose()
    {
        _consumer.Dispose();
    }
}