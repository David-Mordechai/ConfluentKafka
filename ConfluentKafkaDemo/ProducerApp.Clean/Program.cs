using ConfluentKafkaDemo.Infrastructure.IocContainer;
using ConfluentKafkaDemo.Infrastructure.Kafka.Builder.Configurations;
using Microsoft.Extensions.DependencyInjection;
using ProducerApp.Clean;

var config = new ProducerConfiguration { BootstrapServers = "localhost:9092" };

var services = new ServiceCollection();
services.AddInfrastructureServices();
services.AddProducerServices(config);
await services
    .AddSingleton<Main>()
    .BuildServiceProvider()
    .GetRequiredService<Main>()
    .Execute("testTopic");