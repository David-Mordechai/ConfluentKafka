#region 
// Kafka framework dependency in our Application logic
#endregion
using Confluent.Kafka;

#region 
// Kafka configuration in our Application logic
#endregion
var config = new ProducerConfig { BootstrapServers = "localhost:9092" };

#region
// Kafka framework creating producer
#endregion
using var p = new ProducerBuilder<Null, string>(config).Build(); 
try
{
    while (true)
    {
        Console.WriteLine("Produce new message: ");
        var messageInput = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(messageInput) is false)
        {
            #region 
            // in the middle of our application logic we have another case of Kafka logic
            #endregion
            var dr = await p.ProduceAsync("testTopic", new Message<Null, string> { Value = messageInput });

            #region 
            // log produce message result => SRP
            #endregion
            Console.WriteLine($"Delivered '{dr.Value}' to '{dr.TopicPartitionOffset}'");
        }
    }
}
#region 
// Kafka related exception 
#endregion
catch (ProduceException<Null, string> e) 
{
    #region 
    // log error, limited to console logger
    #endregion
    Console.WriteLine($"Delivery failed: {e.Error.Reason}");
}
#region 
// Violation of all S.O.L.I.D principals
// Code is not readable, has many hidden intentions, poor naming 
// Code Tightly couple to the Confluent.Kafka framework 
#endregion