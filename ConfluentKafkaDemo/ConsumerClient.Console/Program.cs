using MessageBroker.Core.Services.Interfaces;
using MessageBroker.Infrastructure.IocContainer;
using MessageBroker.Infrastructure.Kafka.Builder.Configurations;
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
services.AddMessageBrokerLoggerServices();
services.AddMessageBrokerConsumerServices(config);
services.AddConsumedMessageDefaultProcessorService();
var serviceProvider = services.BuildServiceProvider();

var consumerService = serviceProvider.GetRequiredService<IConsumerService>();
consumerService.Subscribe("testTopic", cts.Token);