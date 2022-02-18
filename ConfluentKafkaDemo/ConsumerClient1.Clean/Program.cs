using ConfluentKafkaDemo.Infrastructure.IocContainer;
using ConfluentKafkaDemo.Infrastructure.Kafka.Builder.Configurations;
using ConsumerClient1.Clean;
using Microsoft.Extensions.DependencyInjection;

var cts = new CancellationTokenSource();

Console.CancelKeyPress += (_, e) => {
    e.Cancel = true; // prevent the process from terminating.
    cts.Cancel();
};

var config = new ConsumerConfiguration
{
    GroupId = "test-consumer-group1",
    BootstrapServers = "localhost:9092"
};

var services = new ServiceCollection();
services.AddInfrastructureServices();
services.AddConsumerServices(config);
services
    .AddSingleton<Main>()
    .BuildServiceProvider()
    .GetRequiredService<Main>()
    .Execute("testTopic", cts.Token);