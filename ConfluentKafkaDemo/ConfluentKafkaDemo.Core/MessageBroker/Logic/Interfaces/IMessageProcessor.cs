using ConfluentKafkaDemo.Application.MessageBroker.Record;

namespace ConfluentKafkaDemo.Application.MessageBroker.Logic.Interfaces;

public interface IMessageProcessor
{
    void Process(ConsumeResultRecord message);
}