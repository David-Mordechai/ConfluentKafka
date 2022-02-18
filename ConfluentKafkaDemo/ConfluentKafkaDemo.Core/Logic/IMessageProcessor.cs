using ConfluentKafkaDemo.Application.MessageBroker.Record;

namespace ConfluentKafkaDemo.Application.Logic
{
    public interface IMessageProcessor
    {
        void Process(ConsumeResultRecord message);
    }
}
