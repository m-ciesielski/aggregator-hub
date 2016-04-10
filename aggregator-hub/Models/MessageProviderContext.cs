using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aggregator_hub.Models
{
    class MessageProviderContext : IMessageProviderContext
    {
        public AppHttpClient AppHttpClient {get; }

        public MessageProviderContext(AppHttpClient appHttpClient)
        {
            this.AppHttpClient = appHttpClient;
        }
    }
}
