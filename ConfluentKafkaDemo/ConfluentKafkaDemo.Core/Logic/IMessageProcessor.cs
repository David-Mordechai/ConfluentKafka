using ConfluentKafkaDemo.Core.MessageBroker.Record;

namespace ConfluentKafkaDemo.Core.Logic
{
    public interface IMessageProcessor
    {
        void Process(ConsumeResultRecord message);
    }
}
