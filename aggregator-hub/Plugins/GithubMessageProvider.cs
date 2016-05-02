using aggregator_hub.Models;
using Windows.Web.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;

namespace aggregator_hub.Plugins
{
    class GithubMessageProvider : IMessageProvider
    {
        private const string NAME = "GitHub Message Provider";
        private const string GITHUB_API_URI = "https://api.github.com/repos/";

        public string RepositoryName { get; set; }
        public string RepositoryOwner { get; set; }


        public async Task<List<Message>> FetchMessagesAsync(IMessageProviderContext context)
        {
            if (RepositoryName == null || RepositoryOwner == null)
                throw new InvalidOperationException("Both repository name and repository owner must be set in order to fetch messages.");

            HttpClient client = context.AppHttpClient.HttpClient;
            Uri baseUri = new Uri(GITHUB_API_URI + RepositoryOwner + "/" + RepositoryName);
            List<Message> messages = new List<Message>();

            //List<Message> commits = await FetchCommits(client, uri);
            //Uri commitsUri = new Uri(uri.ToString() + "/commits");
            Uri commitsUri = new Uri(baseUri + "/commits");

            //Prepare request message
            HttpRequestMessage commitsRequest = new HttpRequestMessage(HttpMethod.Get, commitsUri);
            commitsRequest.Headers.Add("Accept", "*/*");
            commitsRequest.Headers.Add("User-Agent", "curl/7.46.0");

            //Send the GET request asynchronously and retrieve the response as a string.
            HttpResponseMessage commitsHttpResponse = new HttpResponseMessage();
            string httpResponseBody = "";

            try
            {
                //Send the GET request
                commitsHttpResponse = await client.SendRequestAsync(commitsRequest);
                commitsHttpResponse.EnsureSuccessStatusCode();
                httpResponseBody = await commitsHttpResponse.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                httpResponseBody = "Error: " + ex.HResult.ToString("X") + " Message: " + ex.Message;
            }

            System.Diagnostics.Debug.WriteLine(httpResponseBody);

            // Parse Json Array
            JsonArray jsonResponse = parseJsonArray(httpResponseBody);

            // Parse JsonValues to JsonStrings
            JsonObject[] commitsArray = convertJsonArrayToArrayOfJsonObjects(jsonResponse);
            
            // Create commits Messages
            foreach(JsonObject commitObject in commitsArray)
            {
                messages.Add(createCommitMessage(commitObject));
            }
           
            return messages;
        }

        public string GetName()
        {
            return NAME;
        }
        
        private JsonArray parseJsonArray(String content)
        {
            JsonArray jsonArray = null;

            try
            {
                jsonArray = JsonArray.Parse(content);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }

            return jsonArray;
        }

        private JsonObject[] convertJsonArrayToArrayOfJsonObjects(JsonArray jsonArray)
        {
            JsonObject[] jsonObjectArray = new JsonObject[jsonArray.Count];
            for(int i = 0; i< jsonArray.Count; ++i)
            {
                JsonObject jsonObject;
                JsonObject.TryParse(jsonArray[i].ToString(), out jsonObject);
                jsonObjectArray[i] = jsonObject;
            }

            return jsonObjectArray;
        }

        private Message createCommitMessage(JsonObject commitJson)
        {
            Message commitMessage = new Message();
            IJsonValue sha, message, commiter, url, date;

            commitJson.TryGetValue("sha", out sha);
            commitJson.TryGetValue("html_url", out url);

            JsonObject commitDetailsObject = null;
            try
            {
                commitDetailsObject = commitJson.GetNamedObject("commit");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }

            commitDetailsObject.TryGetValue("message", out message);
             
            JsonObject commitAuthorObject = null;
            try
            {
                commitAuthorObject = commitDetailsObject.GetNamedObject("author");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }

            commitAuthorObject.TryGetValue("date", out date);
            commitAuthorObject.TryGetValue("name", out commiter);

            commitMessage.Content = (message != null ) ? message.ToString().Replace("\"", "") : "";
            commitMessage.Header = (commiter != null) ? commiter.ToString().Replace("\"", "") : "";
            commitMessage.Link = (url != null) ? url.ToString().Replace("\"", "") : "";
            commitMessage.Timestamp = (date != null) ? date.ToString().Replace("\"", ""): "";

            return commitMessage;
        }
    }


}
