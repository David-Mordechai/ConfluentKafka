using Confluent.Kafka;
using ConfluentKafkaDemo.Infrastructure.Kafka.Builder.Configurations;

namespace ConfluentKafkaDemo.Infrastructure.Kafka.Builder;

internal class ProducerBuilderAdapter
{
    private readonly ProducerConfiguration _configuration;

    public ProducerBuilderAdapter(ProducerConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IProducer<Null, string> Build()
    {
        var config = new ProducerConfig { BootstrapServers = _configuration.BootstrapServers };
        return new ProducerBuilder<Null, string>(config).Build();
    }
}