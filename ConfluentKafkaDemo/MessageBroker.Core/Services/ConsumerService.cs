using MessageBroker.Core.Logger;
using MessageBroker.Core.Services.Interfaces;

namespace MessageBroker.Core.Services;

public class ConsumerService : IConsumerService
{
    private readonly ILoggerAdapter<ConsumerService> _logger;
    private readonly IConsumerAdapter _consumer;
    private readonly IMessageProcessor _messageProcessor;

    public ConsumerService(ILoggerAdapter<ConsumerService> logger, IConsumerAdapter consumer,
        IMessageProcessor messageProcessor)
    {
        _logger = logger;
        _consumer = consumer;
        _messageProcessor = messageProcessor;
    }

    public void Subscribe(string messageType, CancellationToken cancellationToken)
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
            }
        }
    }
}