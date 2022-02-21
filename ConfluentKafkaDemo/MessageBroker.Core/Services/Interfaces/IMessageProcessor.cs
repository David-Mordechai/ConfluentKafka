using MessageBroker.Core.Models;

namespace MessageBroker.Core.Services.Interfaces;

public interface IMessageProcessor
{
    bool Process(ConsumeResultModel message);
}