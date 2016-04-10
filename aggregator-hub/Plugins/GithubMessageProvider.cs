using aggregator_hub.Models;
using Windows.Web.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aggregator_hub.Plugins
{
    class GithubMessageProvider : IMessageProvider
    {
        private const string NAME = "GitHub Message Provider";
        private const string GITHUB_API_URI = "https://api.github.com/repos";

        public string RepositoryName { get; set; }
        public string RepositoryOwner { get; set; }


        public async Task<List<Message>> FetchMessagesAsync(IMessageProviderContext context)
        {
            if (RepositoryName == null || RepositoryOwner == null)
                throw new InvalidOperationException("Both repository name and repository owner must be set in order to fetch messages.");

            HttpClient client = context.AppHttpClient.HttpClient;
            Uri uri = new Uri(GITHUB_API_URI + RepositoryOwner + "/" + RepositoryName);
            List<Message> messages = new List<Message>();

            List<Message> commits = await FetchCommits(client, uri);

            messages.Union(commits);

            return messages;
        }

        public string GetName()
        {
            return NAME;
        }

        private async Task<List<Message>> FetchCommits(HttpClient client, Uri uri)
        {
            Uri commitsUri = new Uri(uri.ToString() + "/commits");
            List<Message> commits = new List<Message>();

            try
            {
                HttpResponseMessage response = await client.GetAsync(commitsUri);
                response.ToString();

            }
            catch
            {

            }


            return commits;
        }

        /*
        static int Main(string[] args)
        {
            IMessageProviderContext context = new MessageProviderContext(new AppHttpClient());
            GithubMessageProvider gitHubMessageProvider = new GithubMessageProvider();
            gitHubMessageProvider.RepositoryOwner = "logistics-mgmt";
            gitHubMessageProvider.RepositoryName = "logistics-mgmt";
            var fetchMessagesTask = gitHubMessageProvider.FetchMessagesAsync(context);

            var result = fetchMessagesTask.Result;

            return 0;
        }
        */
    }


}
