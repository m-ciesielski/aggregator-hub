using aggregator_hub.Models;
using aggregator_hub.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Shapes;

namespace aggregator_hub.Views
{
    abstract class MessagesContainerViewFactory
    {
        private const float DEFAULT_LIST_VIEW_HEIGHT = 500;
        private const float DEFAULT_HUB_SECTION_WIDTH = 200;

        public static MessagesContainerView createMessagesContainerView(MessagesContainerViewModel viewModel, Hub hub)
        {
            MessagesContainerView view = new MessagesContainerView();

            view.HubSection = new HubSection();
            view.HubSection.Header = "factoryHub";
            view.HubSection.Width = 300;
            //hs.Height = 500;
            view.HubSection.ContentTemplate = (DataTemplate) App.Current.Resources["MessagesViewDataTemplate"];
            view.HubSection.DataContext = viewModel;

           // view.UpdateButton = view.HubSection.

            hub.Sections.Add(view.HubSection);

            return view;
        }
    }
}
