using ConfluentKafkaDemo.Application.InputOutput;
using ConfluentKafkaDemo.Application.Logger;
using ConfluentKafkaDemo.Application.MessageBroker;
using ConfluentKafkaDemo.Application.MessageBroker.Logic;
using ConfluentKafkaDemo.Application.MessageBroker.Logic.Interfaces;
using ConfluentKafkaDemo.Application.MessageBroker.Services;
using ConfluentKafkaDemo.Application.MessageBroker.Services.Interfaces;
using ConfluentKafkaDemo.Application.MessageBroker.Validation;
using ConfluentKafkaDemo.Application.MessageBroker.Validation.Interfaces;
using ConfluentKafkaDemo.Infrastructure.Kafka;
using ConfluentKafkaDemo.Infrastructure.Kafka.Builder;
using ConfluentKafkaDemo.Infrastructure.Kafka.Builder.Configurations;
using ConfluentKafkaDemo.Infrastructure.Logger;
using ConfluentKafkaDemo.Infrastructure.WindowsConsole;
using Microsoft.Extensions.DependencyInjection;

namespace ConfluentKafkaDemo.Infrastructure.IocContainer;

public static class ServicesCollectionExtension
{
    public static void AddInfrastructureCommonServices(this IServiceCollection services)
    {
        services.AddSingleton<ILoggerAdapter, ConsoleLogger>();
    }

    public static void AddInfrastructureProducerServices(this IServiceCollection services, 
        ProducerConfiguration producerConfiguration)
    {
        services.AddSingleton<IProducerService, ProducerService>();
        services.AddSingleton<IConsoleAdapter, ConsoleAdapter>();
        services.AddSingleton<IGenerateMessage, GenerateMessageFromConsole>();
        services.AddSingleton<IMessageValidator, StringMessageValidator>();

        services.AddSingleton<IProducerAdapter>(new ProducerAdapter(
            new ProducerBuilderAdapter(producerConfiguration).Build()));
    }

    public static void AddInfrastructureConsumerServices(this IServiceCollection services, 
        ConsumerConfiguration consumerConfiguration)
    {
        services.AddSingleton<IConsumerService, ConsumerService>();
        services.AddSingleton<IMessageProcessor, MessageProcessor>();
        
        services.AddSingleton<IConsumerAdapter> (new ConsumerAdapter(
            new ConsumerBuilderAdapter(consumerConfiguration).Build()));
    }
}