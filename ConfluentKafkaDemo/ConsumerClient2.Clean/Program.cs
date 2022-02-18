using ConfluentKafkaDemo.Infrastructure;
using ConfluentKafkaDemo.Infrastructure.Kafka.Builder.Configurations;
using ConsumerClient2.Clean;
using Microsoft.Extensions.DependencyInjection;

var cts = new CancellationTokenSource();

Console.CancelKeyPress += (_, e) => {
    e.Cancel = true; // prevent the process from terminating.
    cts.Cancel();
};

var config = new ConsumerConfiguration
{
    GroupId = "test-consumer-group2",
    BootstrapServers = "localhost:9092"
};

var services = new ServiceCollection();
services.AddInfrastructureServices();
services.AddConsumerServices(config);
services
    .AddSingleton<MessagesConsumerService, MessagesConsumerService>()
    .BuildServiceProvider()
    .GetRequiredService<MessagesConsumerService>()
    .Execute("testTopic", cts.Token);