namespace ConfluentKafkaDemo.Core.Logger;

public interface ILoggerAdapter
{
    void LogInformation(string log);
    void LogError(string error);

}