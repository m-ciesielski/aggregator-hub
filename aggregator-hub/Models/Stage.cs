using aggregator_hub.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aggregator_hub.Models
{
    class Stage
    {
        public string Name { get; set; }
        public List<MessagesContainer> Containers { get; set; }
        public string BackgroundImage { get; set; }
        public MessageProviderManager MessageProviderManager { get; }

        public Stage(MessageProviderManager messageProviderManager)
        {
            this.MessageProviderManager = messageProviderManager;

            // Create blank container
            this.Containers = new List<MessagesContainer>();
            this.Containers.Add(new MessagesContainer(MessageProviderManager.Context));
        }

    }
}
