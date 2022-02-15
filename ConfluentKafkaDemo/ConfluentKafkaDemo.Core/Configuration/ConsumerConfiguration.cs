namespace ConfluentKafkaDemo.Core.Configuration
{
    public class ConsumerConfiguration
    {
        public string? GroupId { get; set; }
        public string? BootstrapServers { get; set; }
    }
}
