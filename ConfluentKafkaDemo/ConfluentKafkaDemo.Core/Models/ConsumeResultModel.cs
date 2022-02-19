namespace MessageBroker.Core.Models;

public record ConsumeResultModel(string Message, string TopicPartitionOffset)
{
    public override string ToString()
    {
        return $"Consumed message '{Message}', at: '{TopicPartitionOffset}'";
    }
}