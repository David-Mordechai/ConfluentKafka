namespace ConfluentKafkaDemo.Core.MessageBroker.Record;

public record DeliveryResultRecord(string Message, string TopicPartitionOffset)
{
    public override string ToString()
    {
        return $"Delivered '{Message}' to '{TopicPartitionOffset}'";
    }
}