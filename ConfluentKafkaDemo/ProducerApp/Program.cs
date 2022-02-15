﻿using Confluent.Kafka;

var config = new ProducerConfig { BootstrapServers = "localhost:9092" };

// If serializers are not specified, default serializers from
// `Confluent.Kafka.Serializers` will be automatically used where
// available. Note: by default strings are encoded as UTF8.
using var p = new ProducerBuilder<Null, string>(config).Build();
try
{
    while (true)
    {
        Console.WriteLine("Produce new message: ");
        var messageInput = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(messageInput) is false)
        {
            var dr = await p.ProduceAsync("testTopic", new Message<Null, string> { Value = messageInput });
            Console.WriteLine($"Delivered '{dr.Value}' to '{dr.TopicPartitionOffset}'");
        }
    }
}
catch (ProduceException<Null, string> e)
{
    Console.WriteLine($"Delivery failed: {e.Error.Reason}");
}