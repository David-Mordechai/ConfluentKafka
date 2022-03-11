using MessageBroker.Core.Models;

namespace MessageBroker.Core.Services.Interfaces;

public interface IConsumerAdapter : IDisposable
{
    void Subscribe(string topic);
    void Consume(CancellationToken cancellationToken, Action<ConsumeResultModel> consumeMessageHandler);
}