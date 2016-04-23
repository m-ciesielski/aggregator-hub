using aggregator_hub.Models;
using aggregator_hub.Plugins;
using aggregator_hub.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace aggregator_hub
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            List<Message> messages = new List<Message>();
            messages.Add(new Message("Test", "123131233", "Test messsage content", "abcd"));
            messages.Add(new Message("Test", "123131233", "Test messsage content", "abcd"));
            messages.Add(new Message("Test", "123131233", "Test messsage content", "abcd"));

            IMessageProviderContext context = new MessageProviderContext(new AppHttpClient());
            GithubMessageProvider gitHubMessageProvider = new GithubMessageProvider();
            gitHubMessageProvider.RepositoryOwner = "logistics-mgmt";
            gitHubMessageProvider.RepositoryName = "logistics-mgmt";

        }

        private void gridView1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
