using aggregator_hub.Managers;
using aggregator_hub.Models;
using aggregator_hub.Plugins;
using aggregator_hub.ViewModels;
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

            //Initialize main components
            AppManager appManager = new AppManager();
            Stage initialStage = appManager.CurrentStage;

            GithubMessageProvider gitHubMessageProvider = new GithubMessageProvider();
            gitHubMessageProvider.RepositoryOwner = "logistics-mgmt";
            gitHubMessageProvider.RepositoryName = "logistics-mgmt";

            MessagesContainer testContainer = new MessagesContainer(appManager.MessageProviderManager.Context);
            testContainer.MessageProvider = gitHubMessageProvider;
            initialStage.Containers.Add(testContainer);
            testContainer.Messages.Add(new Message("testHeader", "1234", "testContent", "http://test.pl"));
            initialStage.updateContainers();

            MessagesContainerViewModel testContainerViewModel = new MessagesContainerViewModel(testContainer);
            HubSection0.DataContext = testContainerViewModel;


            // Dynamically created container MVVM 
            GithubMessageProvider newtonMessagesProvider = new GithubMessageProvider();
            newtonMessagesProvider.RepositoryOwner = "m-ciesielski";
            newtonMessagesProvider.RepositoryName = "newtons-interpolation";

            MessagesContainer newtonContainer = new MessagesContainer(appManager.MessageProviderManager.Context);
            newtonContainer.MessageProvider = newtonMessagesProvider;
            initialStage.Containers.Add(newtonContainer);
            initialStage.updateContainers();

            MessagesContainerViewModel newtonViewModel = new MessagesContainerViewModel(newtonContainer);

            MessagesContainerView newtonView = MessagesContainerViewFactory.createMessagesContainerView(newtonViewModel, StageHub);

            // Dynamically created container MVVM - Slack
            SlackMessageProvider slackMessagesProvider = new SlackMessageProvider();
            slackMessagesProvider.AuthToken = "xoxp-14123929075-14126498321-39212835763-6796628539";
            slackMessagesProvider.Channel = "C0E3RLZKJ";

            MessagesContainer slackMessagesContainer = new MessagesContainer(appManager.MessageProviderManager.Context);
            slackMessagesContainer.MessageProvider = slackMessagesProvider;
            initialStage.Containers.Add(slackMessagesContainer);
            initialStage.updateContainers();

            MessagesContainerViewModel slackMessagesViewModel = new MessagesContainerViewModel(slackMessagesContainer);

            MessagesContainerView slackMessagesView = MessagesContainerViewFactory.createMessagesContainerView(slackMessagesViewModel, StageHub);


            testContainerViewModel.ToString();

        }

        private void gridView1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void updateButton_click(object sender, RoutedEventArgs e)
        {

        }
    }
}
