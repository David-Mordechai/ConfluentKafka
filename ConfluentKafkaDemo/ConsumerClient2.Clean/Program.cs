using ConfluentKafkaDemo.Application.MessageBroker.Services.Interfaces;
using ConfluentKafkaDemo.Infrastructure.IocContainer;
using ConfluentKafkaDemo.Infrastructure.Kafka.Builder.Configurations;
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
services.AddInfrastructureCommonServices();
services.AddInfrastructureConsumerServices(config);
var serviceProvider = services.BuildServiceProvider();

var consumerService = serviceProvider.GetRequiredService<IConsumerService>();
consumerService.Start("testTopic", cts.Token);