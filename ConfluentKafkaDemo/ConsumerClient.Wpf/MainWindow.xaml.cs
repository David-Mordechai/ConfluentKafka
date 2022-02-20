using System.Collections.ObjectModel;
using System.Windows.Data;
using ConsumerClient.Wpf.ViewModel;
using MessageBroker.Core.Logic.Interfaces;

namespace ConsumerClient.Wpf;

public partial class MainWindow
{
    private readonly object _locker = new();
    public MainWindow(IMessageProcessor messageProcessor)
    {
        InitializeComponent();
        var viewModel = messageProcessor as MessagesViewModel;
        DataContext = viewModel;
        var messagesList = viewModel?.Messages ?? new ObservableCollection<MessagesViewModel.MessageRecord>();
        BindingOperations.EnableCollectionSynchronization(messagesList, _locker);
    }
}