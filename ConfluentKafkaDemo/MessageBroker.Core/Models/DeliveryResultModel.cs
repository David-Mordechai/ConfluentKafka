namespace MessageBroker.Core.Models;

public record DeliveryResultModel(string Message, string TopicPartitionOffset)
{
    public override string ToString()
    {
        return $"Delivered '{Message}' to '{TopicPartitionOffset}'";
    }
}