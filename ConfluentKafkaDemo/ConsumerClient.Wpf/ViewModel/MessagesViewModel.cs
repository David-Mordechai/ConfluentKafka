﻿using System.Collections.ObjectModel;
using MessageBroker.Core.Logic.Interfaces;
using MessageBroker.Core.Models;

namespace ConsumerClient.Wpf.ViewModel;

internal class MessagesViewModel : IMessageProcessor
{
    internal record MessageRecord(string Message);

    public ObservableCollection<MessageRecord> Messages { get; } = new();
    
    public bool Process(ConsumeResultModel message)
    {
        Messages.Add(new MessageRecord(message.ToString()));
        return true;
    }
}

