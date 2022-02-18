using ConfluentKafkaDemo.Application.InputOutput;

namespace ConfluentKafkaDemo.Infrastructure.WindowsConsole;

public class ConsoleAdapter : IConsoleAdapter
{
    public string ReadLine()
    {
        var input = Console.ReadLine();
        return input ?? string.Empty;
    }

    public void WriteLine(string message)
    {
        Console.WriteLine(message);
    }
}