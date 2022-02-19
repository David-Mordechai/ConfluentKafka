using MessageBroker.Core.Enums;
using MessageBroker.Core.Logger;
using MessageBroker.Core.Logic;
using MessageBroker.Core.Logic.Interfaces;
using MessageBroker.Core.Services;
using MessageBroker.Core.Services.Interfaces;
using MessageBroker.Core.Validation;
using MessageBroker.Core.Validation.Interfaces;
using MessageBroker.Infrastructure.InputOutput;
using MessageBroker.Infrastructure.Kafka;
using MessageBroker.Infrastructure.Kafka.Builder;
using MessageBroker.Infrastructure.Kafka.Builder.Configurations;
using MessageBroker.Infrastructure.Logger;
using Microsoft.Extensions.DependencyInjection;

namespace MessageBroker.Infrastructure.IocContainer;

public static class ServicesCollectionExtension
{
    public static void AddMessageBrokerLoggerServices(this IServiceCollection services, 
        LoggerType loggerType = LoggerType.ConsoleLogger)
    {
        switch (loggerType)
        {
            case LoggerType.ConsoleLogger:
                services.AddSingleton(typeof(ILoggerAdapter<>), typeof(ConsoleLogger<>));
                break;
            case LoggerType.DotnetLogger:
                services.AddSingleton(typeof(ILoggerAdapter<>), typeof(DotnetLogger<>));
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(loggerType), loggerType, null);
        }
    }

    public static void AddMessageBrokerProducerServices(this IServiceCollection services, 
        ProducerConfiguration producerConfiguration)
    {
        services.AddScoped<IProducerService, ProducerService>();
        services.AddScoped<IGenerateMessage, GenerateMessageFromConsole>();
        services.AddScoped<IMessageValidator, StringMessageValidator>();
        services.AddScoped(_ => new ProducerBuilderAdapter(producerConfiguration));
        services.AddScoped<IProducerAdapter, ProducerAdapter>();
    }

    public static void AddMessageBrokerConsumerServices(this IServiceCollection services, 
        ConsumerConfiguration consumerConfiguration)
    {
        services.AddScoped<IConsumerService, ConsumerService>();
        services.AddScoped(_ => new ConsumerBuilderAdapter(consumerConfiguration));
        services.AddScoped<IConsumerAdapter, ConsumerAdapter>();
    }

    public static void AddConsumedMessageDefaultProcessorService(this IServiceCollection services)
    {
        services.AddScoped<IMessageProcessor, MessageProcessor>();
    }
}