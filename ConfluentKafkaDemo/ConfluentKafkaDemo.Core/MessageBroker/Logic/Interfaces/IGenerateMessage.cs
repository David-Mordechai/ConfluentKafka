using ConfluentKafkaDemo.Application.MessageBroker.Record;

namespace ConfluentKafkaDemo.Application.MessageBroker.Logic.Interfaces;

public interface IGenerateMessage
{
    MessageRecord Generate();
}