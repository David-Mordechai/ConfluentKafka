using ConfluentKafkaDemo.Core.Logger;
using ConfluentKafkaDemo.Core.MessageBroker;
using ConfluentKafkaDemo.Infrastructure.IocContainer;
using ConfluentKafkaDemo.Infrastructure.Kafka.Builder.Configurations;
using Microsoft.Extensions.DependencyInjection;

var config = new ProducerConfiguration { BootstrapServers = "localhost:9092" };

var services = new ServiceCollection();
services.AddInfrastructureServices();
services.AddProducerServices(config);
await services
    .AddSingleton<Main, Main>()
    .BuildServiceProvider()
    .GetRequiredService<Main>()
    .Execute("testTopic");

internal class Main
{
    private readonly ILoggerAdapter _logger;
    private readonly IProducerAdapter _producer;

    public Main(ILoggerAdapter logger, IProducerAdapter producer)
    {
        _logger = logger;
        _producer = producer;
    }

    public async Task Execute(string messageType)
    {
        try
        {
            while (true)
            {
                Console.WriteLine("Produce new message: ");
                var messageInput = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(messageInput) is not false) continue;

                var deliveryResult = await _producer.ProduceAsync(messageType, messageInput);
                _logger.LogInformation($"Delivery success: {deliveryResult}");
            }
        }
        catch (Exception e)
        {
            _logger.LogError($"Delivery failed: {e.Message}");
        }
    }
}