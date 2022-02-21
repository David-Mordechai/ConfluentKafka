using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using ConsumerClient.Wpf.ViewModel;
using MessageBroker.Core.Services.Interfaces;
using MessageBroker.Infrastructure.IocContainer;
using MessageBroker.Infrastructure.Kafka.Builder.Configurations;
using Microsoft.Extensions.DependencyInjection;

namespace ConsumerClient.Wpf;

public partial class App
{
    private readonly ServiceProvider _serviceProvider;
    private readonly CancellationTokenSource _cts = new();

    public App()
    {
        var services = new ServiceCollection();
        ConfigureServices(services);
        _serviceProvider = services.BuildServiceProvider();
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddMessageBrokerLoggerServices();
        services.AddMessageBrokerConsumerServices(
            new ConsumerConfiguration
            {
                GroupId = "test-consumer-group2",
                BootstrapServers = "localhost:9092"
            });
        services.AddScoped<IMessageProcessor, MessagesViewModel>();
        services.AddSingleton<MainWindow>();
    }

    private void OnStartup(object sender, StartupEventArgs e)
    {
        var consumerService = _serviceProvider.GetRequiredService<IConsumerService>();
        Task.Run(() =>
        {
            consumerService.Subscribe("testTopic", _cts.Token);
        }, _cts.Token);

        var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }

    private void OnExit(object sender, ExitEventArgs e)
    {
        _cts.Cancel();
    }
}