using ConfluentKafkaDemo.Application.InputOutput;
using ConfluentKafkaDemo.Application.MessageBroker.Logic.Interfaces;
using ConfluentKafkaDemo.Application.MessageBroker.Record;

namespace ConfluentKafkaDemo.Application.MessageBroker.Logic;

public class GenerateMessageFromConsole : IGenerateMessage
{
    private readonly IConsoleAdapter _console;

    public GenerateMessageFromConsole(IConsoleAdapter console)
    {
        _console = console;
    }

    public MessageRecord Generate()
    {
        _console.WriteLine("Produce new message: ");
        return new MessageRecord(Value: _console.ReadLine());
    }
}