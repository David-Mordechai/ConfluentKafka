using Confluent.Kafka; // Kafka framework dependency in our Application logic

var config = new ProducerConfig { BootstrapServers = "localhost:9092" }; // Kafka configuration in our Application logic

using var p = new ProducerBuilder<Null, string>(config).Build(); // Kafka framework creating producer
try // our application logic begin here
{
    while (true)
    {
        Console.WriteLine("Produce new message: "); // prompt user to produce a message
        var messageInput = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(messageInput) is false)
        {
            // in the middle of our application logic we have Kafka another case of Kafka logic
            var dr = await p.ProduceAsync("testTopic", new Message<Null, string> { Value = messageInput }); 
            
            // log produce message result
            Console.WriteLine($"Delivered '{dr.Value}' to '{dr.TopicPartitionOffset}'");
        }
    }
}
catch (ProduceException<Null, string> e) // Kafka related exception 
{
    // log error 
    Console.WriteLine($"Delivery failed: {e.Error.Reason}");
}

// Violation of all S.O.L.I.D principals
// Code is not readable, has many hidden intentions, poor naming 
// Code Tightly couple to the Confluent.Kafka framework 