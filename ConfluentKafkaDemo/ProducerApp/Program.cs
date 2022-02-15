using System.Text.Json;
using Confluent.Kafka;

var config = new ProducerConfig { BootstrapServers = "localhost:9092" };

// If serializers are not specified, default serializers from
// `Confluent.Kafka.Serializers` will be automatically used where
// available. Note: by default strings are encoded as UTF8.
using var p = new ProducerBuilder<Null, string>(config).Build();
try
{
    while (true)
    {
        Console.WriteLine("Produce new message: ");
        var message = Console.ReadLine();
        if (message != null)
        {
            var messageRecord = new MessageRecord(message, DateTime.Now);
            var messageJson = JsonSerializer.Serialize(messageRecord);
            var dr = await p.ProduceAsync("testTopic", new Message<Null, string> { Value = messageJson });
            Console.WriteLine($"Delivered '{dr.Value}' to '{dr.TopicPartitionOffset}'");
        }
    }
}
catch (ProduceException<Null, string> e)
{
    Console.WriteLine($"Delivery failed: {e.Error.Reason}");
}

internal record MessageRecord(string Message, DateTime MessageDate = new DateTime());