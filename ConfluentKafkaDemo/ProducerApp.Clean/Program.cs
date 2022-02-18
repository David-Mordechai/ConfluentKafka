using ConfluentKafkaDemo.Infrastructure;
using ConfluentKafkaDemo.Infrastructure.Kafka.Builder.Configurations;
using Microsoft.Extensions.DependencyInjection;
using ProducerApp.Clean;

var config = new ProducerConfiguration { BootstrapServers = "localhost:9092" };

var services = new ServiceCollection();
services.AddInfrastructureServices();
services.AddProducerServices(config);
await services
    .AddSingleton<MessageProducerService, MessageProducerService>()
    .BuildServiceProvider()
    .GetRequiredService<MessageProducerService>()
    .Execute("testTopic");