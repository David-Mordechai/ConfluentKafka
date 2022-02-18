using ConfluentKafkaDemo.Core.Logger;
using ConfluentKafkaDemo.Core.Logic;
using ConfluentKafkaDemo.Core.MessageBroker;
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
    GroupId = "test-consumer-group1",
    BootstrapServers = "localhost:9092"
};

#region IocContainer
var services = new ServiceCollection();
services.AddInfrastructureServices();
services.AddConsumerServices(config);
services
    .AddSingleton<Main, Main>()
    .BuildServiceProvider()
    .GetRequiredService<Main>()
    .Execute("testTopic", cts.Token);
#endregion

internal class Main
{
    private readonly ILoggerAdapter _logger;
    private readonly IConsumerAdapter _consumer;
    private readonly IMessageProcessor _messageProcessor;

    public Main(ILoggerAdapter logger, IConsumerAdapter consumer,
        IMessageProcessor messageProcessor)
    {
        _logger = logger;
        _consumer = consumer;
        _messageProcessor = messageProcessor;
    }

    public void Execute(string messageType, CancellationToken cancellationToken)
    {
        _consumer.Subscribe(messageType);

        while (cancellationToken.IsCancellationRequested is false)
        {
            try
            {
                var consumeResult = _consumer.Consume(cancellationToken);
                _messageProcessor.Process(consumeResult);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                if (e is OperationCanceledException)
                    break;
            }
        }
    }
}