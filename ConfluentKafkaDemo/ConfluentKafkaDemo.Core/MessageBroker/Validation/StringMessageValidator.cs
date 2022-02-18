using ConfluentKafkaDemo.Application.MessageBroker.Validation.Interfaces;

namespace ConfluentKafkaDemo.Application.MessageBroker.Validation;

public class StringMessageValidator : IMessageValidator
{
    public bool Valid(string messageInput)
    {
        return string.IsNullOrWhiteSpace(messageInput) is false;
    }
}