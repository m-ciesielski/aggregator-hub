using aggregator_hub.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace aggregator_hub.ViewModels
{
    class MessagesContainerViewModel
    {
        public ObservableCollection<Message> Messages { get; set; }


        public MessagesContainerViewModel(ObservableCollection<Message> messages)
        {
            this.Messages = messages;
        }

    }
}
