namespace ConfluentKafkaDemo.Core.Record
{
    public record ConsumeResultRecord(string Message, string TopicPartitionOffset);
}
