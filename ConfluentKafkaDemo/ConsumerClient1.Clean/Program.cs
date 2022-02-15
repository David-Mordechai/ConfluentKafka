using ConfluentKafkaDemo.Core;
using ConfluentKafkaDemo.Core.Configuration;
using ConfluentKafkaDemo.Infrastructure.Builder;

var config = new ConsumerConfiguration
{
    GroupId = "test-consumer-group1",
    BootstrapServers = "localhost:9092"
};

using IConsumerAdapter consumer = new ConsumerBuilderAdapter(config).Build();
consumer.Subscribe("testTopic");

var cts = new CancellationTokenSource();

Console.CancelKeyPress += (_, e) => {
    e.Cancel = true; // prevent the process from terminating.
    cts.Cancel();
};

while (true)
{
    try
    {
        var cr = consumer.Consume(cts.Token);
        Console.WriteLine(
            $"Consumed message '{cr?.Message}', at: '{cr?.TopicPartitionOffset}'");
    }
    catch (Exception e)
    {
        Console.WriteLine($"Error occurred: {e.Message}");
        if (e is OperationCanceledException)
            break;
    }
}