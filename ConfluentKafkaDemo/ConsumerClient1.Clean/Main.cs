using ConfluentKafkaDemo.Application.Logger;
using ConfluentKafkaDemo.Application.Logic;
using ConfluentKafkaDemo.Application.MessageBroker;

namespace ConsumerClient1.Clean;

internal class Main
{
    private readonly ILoggerAdapter _logger;
    private readonly IConsumerAdapter _consumer;
    private readonly IMessageProcessor _messageProcessor;

    public Main(ILoggerAdapter logger, IConsumerAdapter consumer,
        IMessageProcessor messageProcessor)
    {
        _logger = logger;
        _consumer = consumer;
        _messageProcessor = messageProcessor;
    }

    public void Execute(string messageType, CancellationToken cancellationToken)
    {
        _consumer.Subscribe(messageType);

        while (cancellationToken.IsCancellationRequested is false)
        {
            try
            {
                var consumeResult = _consumer.Consume(cancellationToken);
                _messageProcessor.Process(consumeResult);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                if (e is OperationCanceledException)
                    break;
            }
        }
    }
}