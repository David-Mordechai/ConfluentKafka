using System.Collections.ObjectModel;
using MessageBroker.Core.Logger;
using MessageBroker.Core.Models;
using MessageBroker.Core.Services.Interfaces;

namespace ConsumerClient.Wpf.ViewModel;

internal class MessagesViewModel : IMessageProcessor
{
    private readonly ILoggerAdapter<MessagesViewModel> _logger;

    public MessagesViewModel(ILoggerAdapter<MessagesViewModel> logger)
    {
        _logger = logger;
    }
    internal record MessageRecord(string Message);

    public ObservableCollection<MessageRecord> Messages { get; } = new();
    
    public (bool success, string errorMessage) Process(ConsumeResultModel message)
    {
        _logger.LogInformation($"New message arrived! - {message}");
        Messages.Add(new MessageRecord(message.ToString()));
        _logger.LogInformation($"Message consumed! - {message}");
        return (success: true, errorMessage: string.Empty);
    }
}

