using MessageBroker.Core.Logic.Interfaces;
using MessageBroker.Core.Models;

namespace MessageBroker.Infrastructure.InputOutput;

public class GenerateMessageFromConsole : IGenerateMessage
{
    public MessageModel Generate()
    {
        Console.WriteLine("Produce new message: ");
        return new MessageModel(Value: Console.ReadLine() ?? string.Empty);
    }
}