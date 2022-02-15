using Confluent.Kafka;
using ConfluentKafkaDemo.Core;
using ConfluentKafkaDemo.Core.Builder;
using ConfluentKafkaDemo.Core.Configuration;

namespace ConfluentKafkaDemo.Infrastructure.Builder
{
    public class ProducerBuilderAdapter : IProducerBuilderAdapter
    {
        private readonly ProducerConfiguration _configuration;

        public ProducerBuilderAdapter(ProducerConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IProducerAdapter Build()
        {
            var config = new ProducerConfig { BootstrapServers = _configuration.BootstrapServers };
            var producer = new ProducerBuilder<Null, string>(config).Build();

            var producerAdapter = new ProducerAdapter(producer);
            return producerAdapter;
        }
    }
}