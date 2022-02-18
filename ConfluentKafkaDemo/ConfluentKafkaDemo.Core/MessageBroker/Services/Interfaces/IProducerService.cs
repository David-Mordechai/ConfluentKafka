namespace ConfluentKafkaDemo.Application.MessageBroker.Services.Interfaces
{
    public interface IProducerService
    {
        Task Start(string messageType);
    }
}
