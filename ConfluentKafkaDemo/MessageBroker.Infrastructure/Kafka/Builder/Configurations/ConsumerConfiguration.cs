namespace MessageBroker.Infrastructure.Kafka.Builder.Configurations;

public class ConsumerConfiguration
{
    public string? GroupId { get; set; }
    public string? BootstrapServers { get; set; }
}