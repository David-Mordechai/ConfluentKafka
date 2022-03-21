using MessageBroker.Core.Enums;
using MessageBroker.Core.Logger;
using MessageBroker.Core.Services;
using MessageBroker.Core.Services.Interfaces;
using MessageBroker.Core.Validation;
using MessageBroker.Core.Validation.Interfaces;
using MessageBroker.Infrastructure.Kafka;
using MessageBroker.Infrastructure.Kafka.Builder;
using MessageBroker.Infrastructure.Kafka.Builder.Configurations;
using MessageBroker.Infrastructure.Logger;
using MessageBroker.Infrastructure.Redis;
using MessageBroker.Infrastructure.Redis.Builder;
using MessageBroker.Infrastructure.Redis.Builder.Configuration;
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

    public static void AddMessageBrokerProducerServicesKafka(this IServiceCollection services, 
        KafkaProducerConfiguration kafkaProducerConfiguration)
    {
        services.AddScoped<IProducerService, ProducerService>();
        services.AddScoped<IMessageValidator, StringMessageValidator>();
        services.AddScoped(_ => new KafkaProducerBuilderAdapter(kafkaProducerConfiguration));
        services.AddScoped<IProducerAdapter, KafkaProducerAdapter>();
    }

    public static void AddMessageBrokerConsumerServicesKafka(this IServiceCollection services, 
        KafkaConsumerConfiguration kafkaConsumerConfiguration)
    {
        services.AddScoped<IConsumerService, ConsumerService>();
        services.AddScoped(_ => new KafkaConsumerBuilderAdapter(kafkaConsumerConfiguration));
        services.AddScoped<IConsumerAdapter, KafkaConsumerAdapter>();
    }

    public static void AddMessageBrokerProducerServicesRedis(this IServiceCollection services,
        RedisConfiguration redisConfiguration)
    {
        services.AddScoped<IProducerService, ProducerService>();
        services.AddScoped<IMessageValidator, StringMessageValidator>();
        services.AddScoped(_ => new RedisBuilderAdapter(redisConfiguration));
        services.AddScoped<IProducerAdapter, RedisProducerAdapter>();
    }

    public static void AddMessageBrokerConsumerServicesRedis(this IServiceCollection services,
        RedisConfiguration redisConfiguration)
    {
        services.AddScoped<IConsumerService, ConsumerService>();
        services.AddScoped(_ => new RedisBuilderAdapter(redisConfiguration));
        services.AddScoped<IConsumerAdapter, RedisConsumerAdapter>();
    }
}