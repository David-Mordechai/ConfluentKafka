using MessageBroker.Core.Models;

namespace MessageBroker.Core.Logic.Interfaces;

public interface IGenerateMessage
{
    MessageModel Generate();
}