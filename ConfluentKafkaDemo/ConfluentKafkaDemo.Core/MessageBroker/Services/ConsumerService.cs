using ConfluentKafkaDemo.Application.Logger;
using ConfluentKafkaDemo.Application.MessageBroker.Logic.Interfaces;
using ConfluentKafkaDemo.Application.MessageBroker.Services.Interfaces;

namespace ConfluentKafkaDemo.Application.MessageBroker.Services
{
    public class ConsumerService : IConsumerService
    {
        private readonly ILoggerAdapter _logger;
        private readonly IConsumerAdapter _consumer;
        private readonly IMessageProcessor _messageProcessor;

        public ConsumerService(ILoggerAdapter logger, IConsumerAdapter consumer,
            IMessageProcessor messageProcessor)
        {
            _logger = logger;
            _consumer = consumer;
            _messageProcessor = messageProcessor;
        }

        public void Start(string messageType, CancellationToken cancellationToken)
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
}
