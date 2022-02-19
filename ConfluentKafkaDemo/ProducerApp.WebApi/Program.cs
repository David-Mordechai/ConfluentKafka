using MessageBroker.Core.Enums;
using MessageBroker.Core.Models;
using MessageBroker.Core.Services.Interfaces;
using MessageBroker.Infrastructure.IocContainer;
using MessageBroker.Infrastructure.Kafka.Builder.Configurations;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMessageBrokerLoggerServices(LoggerType.DotnetLogger);
builder.Services.AddMessageBrokerProducerServices(
    new ProducerConfiguration { BootstrapServers = "localhost:9092" });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPut("/produceMessage", async ([FromServices] IProducerService producerService, MessageModel message) =>
{
    await producerService.Produce(message, "testTopic");
    return Results.Ok();
}).WithName("ProduceMessage");

app.Run();
