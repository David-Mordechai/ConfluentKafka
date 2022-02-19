using MessageBroker.Core.Logger;
using MessageBroker.Core.Logic.Interfaces;
using MessageBroker.Core.Models;

namespace MessageBroker.Core.Logic;

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