namespace MessageBroker.Core.Services.Interfaces;

public interface IConsumerService
{
    void Subscribe(string messageType, CancellationToken cancellationToken);
}