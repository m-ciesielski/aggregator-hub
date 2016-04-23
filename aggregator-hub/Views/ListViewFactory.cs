using aggregator_hub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Shapes;

namespace aggregator_hub.Views
{
    abstract class MessagesListViewFactory
    {
        private const float DEFAULT_LIST_VIEW_HEIGHT = 500;
        private const float DEFAULT_LIST_VIEW_WIDTH = 200;

        public ListView createMessagesListView(List<Message> itemsSource)
        {
            ListView listView = new ListView();
            listView.Height = DEFAULT_LIST_VIEW_HEIGHT;
            listView.Width = DEFAULT_LIST_VIEW_WIDTH;
            listView.ItemsSource = itemsSource;
            return listView;
        }
    }
}
