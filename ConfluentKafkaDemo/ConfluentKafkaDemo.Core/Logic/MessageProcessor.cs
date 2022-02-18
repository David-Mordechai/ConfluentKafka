using ConfluentKafkaDemo.Core.Logger;
using ConfluentKafkaDemo.Core.MessageBroker.Record;

namespace ConfluentKafkaDemo.Core.Logic
{
    public class MessageProcessor : IMessageProcessor
    {
        private readonly ILoggerAdapter _logger;

        public MessageProcessor(ILoggerAdapter logger)
        {
            _logger = logger;
        }

        public void Process(ConsumeResultRecord message)
        {
            _logger.LogInformation(message.ToString());
        }
    }
}
