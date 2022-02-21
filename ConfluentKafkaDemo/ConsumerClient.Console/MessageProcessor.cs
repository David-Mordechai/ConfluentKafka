using MessageBroker.Core.Logger;
using MessageBroker.Core.Models;
using MessageBroker.Core.Services.Interfaces;

namespace ConsumerClient.Console;

public class MessageProcessor : IMessageProcessor
{
    private readonly ILoggerAdapter<MessageProcessor> _logger;

    public MessageProcessor(ILoggerAdapter<MessageProcessor> logger)
    {
        _logger = logger;
    }

    public bool Process(ConsumeResultModel message)
    {
        _logger.LogInformation(message.ToString());
        return true;
    }
}