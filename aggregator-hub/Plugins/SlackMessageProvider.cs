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
    class SlackMessageProvider : IMessageProvider
    {
        private const string NAME = "Slack Message Provider";
        private const string SLACK_API_URL = "https://slack.com/api/";

        public string AuthToken { get; set; }
        public string Channel { get; set; }


        public async Task<List<Message>> FetchMessagesAsync(IMessageProviderContext context)
        {
            if (AuthToken == null )
                throw new InvalidOperationException("Set authorization token in order to fetch Slack messages.");

            HttpClient client = context.AppHttpClient.HttpClient;
            Uri baseUri = new Uri(SLACK_API_URL);
            List<Message> messages = new List<Message>();

            //List<Message> commits = await FetchCommits(client, uri);
            //Uri commitsUri = new Uri(uri.ToString() + "/commits");
            Uri slackMessagesUri = new Uri(baseUri + "channels.history?token=" + AuthToken + "&channel=" + Channel);

            //Prepare request message
            HttpRequestMessage slackMessagesRequest = new HttpRequestMessage(HttpMethod.Get, slackMessagesUri);
            slackMessagesRequest.Headers.Add("Accept", "*/*");
            slackMessagesRequest.Headers.Add("User-Agent", "curl/7.46.0");

            //Send the GET request asynchronously and retrieve the response as a string.
            HttpResponseMessage slackMessagesResponse = new HttpResponseMessage();
            string httpResponseBody = "";

            try
            {
                //Send the GET request
                slackMessagesResponse = await client.SendRequestAsync(slackMessagesRequest);
                slackMessagesResponse.EnsureSuccessStatusCode();
                httpResponseBody = await slackMessagesResponse.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                httpResponseBody = "Error: " + ex.HResult.ToString("X") + " Message: " + ex.Message;
                System.Diagnostics.Debug.WriteLine(httpResponseBody);
                return null;
            }

            System.Diagnostics.Debug.WriteLine(httpResponseBody);

            // Parse response JSON
            JsonObject jsonResponseObject;
            JsonObject.TryParse(httpResponseBody, out jsonResponseObject);
            IJsonValue slackMessagesArrayValue;
            jsonResponseObject.TryGetValue("messages", out slackMessagesArrayValue);

            JsonArray slackMessagesArray;
            JsonArray.TryParse(slackMessagesArrayValue.ToString(), out slackMessagesArray);
            
            // Parse JsonValues to JsonStrings
           JsonObject[] slackMessagesObjectsArray = convertJsonArrayToArrayOfJsonObjects(slackMessagesArray);
            
            // Create actual Messages
            foreach(JsonObject slackMessageObject in slackMessagesObjectsArray)
            {
                messages.Add(createSlackMessage(slackMessageObject));
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

        private Message createSlackMessage(JsonObject slackMessageJson)
        {
            Message slackMessage = new Message();
            IJsonValue user, text, timestamp;

            slackMessageJson.TryGetValue("user", out user);
            slackMessageJson.TryGetValue("text", out text);
            slackMessageJson.TryGetValue("ts", out timestamp);
 
            slackMessage.Content = (text != null) ? text.ToString().Replace("\"", "") : "Empty content";
            slackMessage.Header = (user != null) ? user.ToString().Replace("\"", "") : "Author unknown";
            slackMessage.Timestamp = (timestamp != null) ? timestamp.ToString().Replace("\"", "") : "";

            return slackMessage;
        }
    }


}
