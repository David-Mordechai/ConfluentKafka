using ConfluentKafkaDemo.Application.Logger;
using ConfluentKafkaDemo.Application.MessageBroker.Logic.Interfaces;
using ConfluentKafkaDemo.Application.MessageBroker.Record;

namespace ConfluentKafkaDemo.Application.MessageBroker.Logic;

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