using Confluent.Kafka;

var conf = new ConsumerConfig
{
    GroupId = "test-consumer-group2",
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
            #region
            // kafka way to consume message
            #endregion
            var cr = c.Consume(cts.Token);

            #region
            // process message => Violation of SRP, OPC, hidden  intention
            #endregion
            Console.WriteLine(
                $"Consumed message '{cr?.Message.Value}', at: '{cr?.TopicPartitionOffset}'");
        }
        #region
        // kafka exception
        #endregion
        catch (ConsumeException e)
        {
            #region
            // log error, limited to console logger
            #endregion
            Console.WriteLine($"Error occurred: {e.Error.Reason}");
        }
    }
}
catch (OperationCanceledException)
{
    #region
    // Kafka Logic, we don't need to know this in our application logic, alternative framework may not have this method 
    #endregion
    // Ensure the consumer leaves the group cleanly and final offsets are committed.
    c.Close();
}