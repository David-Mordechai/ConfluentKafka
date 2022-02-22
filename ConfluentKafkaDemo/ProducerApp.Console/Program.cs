using MessageBroker.Core.Models;
using MessageBroker.Core.Services.Interfaces;
using MessageBroker.Infrastructure.IocContainer;
using MessageBroker.Infrastructure.Kafka.Builder.Configurations;
using Microsoft.Extensions.DependencyInjection;

var config = new ProducerConfiguration { BootstrapServers = "localhost:9092" };

var services = new ServiceCollection();
services.AddMessageBrokerLoggerServices();
services.AddMessageBrokerProducerServices(config);
var serviceProvider = services.BuildServiceProvider();

var producerService = serviceProvider.GetRequiredService<IProducerService>();

var cts = new CancellationTokenSource();

Console.CancelKeyPress += (_, e) => {
    e.Cancel = true; // prevent the process from terminating.
    cts.Cancel();
};

while (cts.IsCancellationRequested is false)
{
    Console.Write("Produce new message: ");
    if(cts.IsCancellationRequested) return;
    var message = new MessageModel(Value: Console.ReadLine() ?? string.Empty);
    await producerService.Produce(message, "testTopic");
}