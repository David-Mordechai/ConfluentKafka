﻿using ConfluentKafkaDemo.Application.Logger;
using ConfluentKafkaDemo.Application.Logic;
using ConfluentKafkaDemo.Application.MessageBroker;
using ConfluentKafkaDemo.Infrastructure.Kafka;
using ConfluentKafkaDemo.Infrastructure.Kafka.Builder;
using ConfluentKafkaDemo.Infrastructure.Kafka.Builder.Configurations;
using ConfluentKafkaDemo.Infrastructure.Logger;
using Microsoft.Extensions.DependencyInjection;

namespace ConfluentKafkaDemo.Infrastructure.IocContainer;

public static class ServicesCollectionExtension
{
    public static void AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddSingleton<ILoggerAdapter, ConsoleLogger>();
        services.AddSingleton<IMessageProcessor, MessageProcessor>();
    }

    public static void AddProducerServices(this IServiceCollection services, 
        ProducerConfiguration producerConfiguration)
    {
        services.AddSingleton<IProducerAdapter>(new ProducerAdapter(
            new ProducerBuilderAdapter(producerConfiguration).Build()));
    }

    public static void AddConsumerServices(this IServiceCollection services, 
        ConsumerConfiguration consumerConfiguration)
    {
        services.AddSingleton<IConsumerAdapter> (new ConsumerAdapter(
            new ConsumerBuilderAdapter(consumerConfiguration).Build()));
    }
}