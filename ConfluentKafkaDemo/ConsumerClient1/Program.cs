using Confluent.Kafka;

var conf = new ConsumerConfig
{
    GroupId = "test-consumer-group1",
    BootstrapServers = "localhost:9092",
    AutoOffsetReset = AutoOffsetReset.Earliest
};

using var c = new ConsumerBuilder<Ignore, string>(conf).Build();
c.Subscribe("testTopic");

var cts = new CancellationTokenSource();
Console.CancelKeyPress += (_, e) => {
    e.Cancel = true; // prevent the process from terminating.
    cts.Cancel();
};

try
{
    while (true)
    {
        try
        {
            // kafka way to consume message
            var cr = c.Consume(cts.Token);

            // process message => Violation of SRP, OPC
            Console.WriteLine(
                $"Consumed message '{cr?.Message.Value}', at: '{cr?.TopicPartitionOffset}'");
        }
        catch (ConsumeException e) // kafka exception
        {
            // log error, limited to console logger
            Console.WriteLine($"Error occurred: {e.Error.Reason}");
        }
    }
}
catch (OperationCanceledException) // application logic exception
{
    // Ensure the consumer leaves the group cleanly and final offsets are committed.
    c.Close(); // Kafka Logic, we don't need to know this in our application logic, alternative framework may not have this method 
}