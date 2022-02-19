using MessageBroker.Core.Models;

namespace MessageBroker.Core.Logic.Interfaces;

public interface IMessageProcessor
{
    bool Process(ConsumeResultModel message);
}