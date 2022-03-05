using System.Windows;
using ConsumerClient.Wpf.ConsumerBackgroundServices;
using ConsumerClient.Wpf.ConsumerBackgroundServices.Logic;
using ConsumerClient.Wpf.ViewModel;
using MessageBroker.Core.Enums;
using MessageBroker.Core.Services.Interfaces;
using MessageBroker.Infrastructure.IocContainer;
using MessageBroker.Infrastructure.Kafka.Builder.Configurations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ConsumerClient.Wpf;

public partial class App
{
    private readonly IHost _host;

    public App()
    {
        _host = Host.CreateDefaultBuilder()
            .ConfigureLogging(builder =>
            {
                builder.ClearProviders();
                builder.AddDebug();
            })
            .ConfigureServices(services =>
            {
                services.AddMessageBrokerLoggerServices(LoggerType.DotnetLogger);
                services.AddMessageBrokerConsumerServices(
                    new ConsumerConfiguration
                    {
                        GroupId = "test-consumer-group2",
                        BootstrapServers = "localhost:9092"
                    });
                services.AddScoped<IMessageProcessor, MessageProcessor>();
                services.AddScoped<MessagesViewModel>();
                services.AddSingleton<MainWindow>();
                services.AddHostedService<ConsumerService>();
            }).Build();
    }

    private void OnStartup(object sender, StartupEventArgs e)
    {
        _host.Start();
        var mainWindow = _host.Services.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }
}