using ConfluentKafkaDemo.Application.MessageBroker.Services.Interfaces;
using ConfluentKafkaDemo.Infrastructure.IocContainer;
using ConfluentKafkaDemo.Infrastructure.Kafka.Builder.Configurations;
using Microsoft.Extensions.DependencyInjection;

var config = new ProducerConfiguration { BootstrapServers = "localhost:9092" };

var services = new ServiceCollection();
services.AddInfrastructureCommonServices();
services.AddInfrastructureProducerServices(config);
var serviceProvider = services.BuildServiceProvider();

var producerService = serviceProvider.GetRequiredService<IProducerService>();
await producerService.Start("testTopic");