using aggregator_hub.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace aggregator_hub.Views
{
    class MessagesContainerView
    {
        public MessagesContainerViewModel ViewModel { get; set; }
        public HubSection HubSection { get; set; }
        public Button UpdateButton { get; set; }
        public Button SettingsButton { get; set; }
    }
}
