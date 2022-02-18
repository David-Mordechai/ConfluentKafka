namespace ConfluentKafkaDemo.Application.InputOutput;

public interface IConsoleAdapter
{
    string ReadLine();
    void WriteLine(string message);
}