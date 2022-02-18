using ConfluentKafkaDemo.Application.Logger;

namespace ConfluentKafkaDemo.Infrastructure.Logger;

public class ConsoleLogger : ILoggerAdapter
{
    public void LogInformation(string log)
    {
        Console.WriteLine($"Information: {log}");
    }

    public void LogError(string error)
    {
        Console.WriteLine($"Error occurred: {error}");
    }
}