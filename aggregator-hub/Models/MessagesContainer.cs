using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aggregator_hub.Models
{
    class MessagesContainer
    {
        public string Name { get; set; }
        public List<Message> Messages { get; set; }
        public IMessageProvider MessageProvider { get; set; }

        protected IMessageProviderContext MessageProviderContext;

        public MessagesContainer(IMessageProviderContext messageProviderContext)
        {
            this.MessageProviderContext = messageProviderContext;
        }

        public void update()
        {
           // Messages.Union(MessageProvider.FetchMessagesAsync(MessageProviderContext));
        }
    }
}
