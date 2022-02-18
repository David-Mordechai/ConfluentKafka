namespace ConfluentKafkaDemo.Application.MessageBroker.Record;

public record ConsumeResultRecord(string Message, string TopicPartitionOffset)
{
    public override string ToString()
    {
        return $"Consumed message '{Message}', at: '{TopicPartitionOffset}'";
    }
}