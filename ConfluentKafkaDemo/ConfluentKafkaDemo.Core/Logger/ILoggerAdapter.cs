﻿namespace ConfluentKafkaDemo.Application.Logger;

public interface ILoggerAdapter
{
    void LogInformation(string log);
    void LogError(string error);

}