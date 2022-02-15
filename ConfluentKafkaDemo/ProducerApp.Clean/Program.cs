using ConfluentKafkaDemo.Core;
using ConfluentKafkaDemo.Core.Configuration;
using ConfluentKafkaDemo.Infrastructure.Builder;

var config = new ProducerConfiguration { BootstrapServers = "localhost:9092" };

using IProducerAdapter producer = new ProducerBuilderAdapter(config).Build();

try
{
    while (true)
    {
        Console.WriteLine("Produce new message: ");
        var messageInput = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(messageInput) is false)
        {
            var (message, topicPartitionOffset) = await producer.ProduceAsync("testTopic", messageInput);
            Console.WriteLine($"Delivered '{message}' to '{topicPartitionOffset}'");
        }
    }
}
catch (Exception e)
{
    Console.WriteLine($"Delivery failed: {e.Message}");
}
