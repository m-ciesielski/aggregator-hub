using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Http;

namespace aggregator_hub.Models
{
    class AppHttpClient
    {
        public HttpClient HttpClient { get; set; }
        
        public AppHttpClient()
        {
            this.HttpClient = new HttpClient();
        }
    }
}
