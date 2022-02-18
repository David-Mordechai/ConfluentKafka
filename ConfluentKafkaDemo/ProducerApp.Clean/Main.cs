using ConfluentKafkaDemo.Application.Logger;
using ConfluentKafkaDemo.Application.MessageBroker;

namespace ProducerApp.Clean;

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