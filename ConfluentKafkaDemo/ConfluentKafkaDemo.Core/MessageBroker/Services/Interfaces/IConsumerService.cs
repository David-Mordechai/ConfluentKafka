namespace ConfluentKafkaDemo.Application.MessageBroker.Services.Interfaces
{
    public interface IConsumerService
    {
        void Start(string messageType, CancellationToken cancellationToken);
    }
}
