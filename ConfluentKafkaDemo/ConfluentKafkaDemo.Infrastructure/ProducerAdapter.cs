using Confluent.Kafka;
using ConfluentKafkaDemo.Core;
using ConfluentKafkaDemo.Core.Record;

namespace ConfluentKafkaDemo.Infrastructure
{
    internal class ProducerAdapter : IProducerAdapter
    {
        public ProducerAdapter(IProducer<Null, string> producer)
        {
            Producer = producer;
        }

        public void Dispose()
        {
            Producer.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<DeliveryResultRecord> ProduceAsync(string topic, string message)
        {
            try
            {
                var dr = await Producer.ProduceAsync("testTopic", new Message<Null, string> { Value = message });
                return new DeliveryResultRecord(
                    Message: dr.Message.Value,
                    TopicPartitionOffset:
                    $"{dr.TopicPartitionOffset.Topic} [{dr.TopicPartitionOffset.Partition}] @{dr.TopicPartitionOffset.Offset}");
            }
            catch (ProduceException<Null, string> e)
            {
                throw new Exception(e.Error.Reason);
            }
        }

        internal IProducer<Null, string> Producer { get; set; }
    }
}
