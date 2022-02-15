namespace ConfluentKafkaDemo.Core.Record
{
    public record DeliveryResultRecord(string Message, string TopicPartitionOffset);
}
