namespace ConfluentKafkaDemo.Application.MessageBroker.Validation.Interfaces;

public interface IMessageValidator
{
    bool Valid(string messageInput);
}