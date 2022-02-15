using Confluent.Kafka;
using ConfluentKafkaDemo.Core;
using ConfluentKafkaDemo.Core.Builder;
using ConfluentKafkaDemo.Core.Configuration;

namespace ConfluentKafkaDemo.Infrastructure.Builder
{
    public class ConsumerBuilderAdapter : IConsumerBuilderAdapter
    {
        private readonly ConsumerConfiguration _configuration;

        public ConsumerBuilderAdapter(ConsumerConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IConsumerAdapter Build()
        {
            var conf = new ConsumerConfig
            {
                GroupId = _configuration.GroupId,
                BootstrapServers = _configuration.BootstrapServers,
                // Note: The AutoOffsetReset property determines the start offset in the event
                // there are not yet any committed offsets for the consumer group for the
                // topic/partitions of interest. By default, offsets are committed
                // automatically, so in this example, consumption will only start from the
                // earliest message in the topic 'my-topic' the first time you run the program.
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
            var consumer = new ConsumerBuilder<Ignore, string>(conf).Build();
            var consumerAdapter = new ConsumerAdapter(consumer);
            return consumerAdapter;
        }

    }
}
