using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Messages;
using Prism.Events;
using Prism.Mvvm;

namespace StatusModule
{
    public class StatusBarViewModel : BindableBase
    {
        private string lastMessage = "Hallo Welt!";

        public string LastMessage
        {
            get => lastMessage;
            set => SetProperty(ref lastMessage, value);
        }

        public StatusBarViewModel(IEventAggregator eventAggregator)
        {
            var statusMessageEvent = eventAggregator.GetEvent<PubSubEvent<StatusMessage>>();
            statusMessageEvent.Subscribe(OnMessageReceived);
        }

        private void OnMessageReceived(StatusMessage statusMessage)
        {
            LastMessage = statusMessage.Message;
        }
    }
}
