using aggregator_hub.Managers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aggregator_hub.Models
{
    class Stage
    {
        public string Name { get; set; }
        public ObservableCollection<MessagesContainer> Containers { get; set; }
        public string BackgroundImage { get; set; }
        public float RefreshRate { get; set; }
        public MessageProviderManager MessageProviderManager { get; }

        public Stage(MessageProviderManager messageProviderManager)
        {
            this.MessageProviderManager = messageProviderManager;
            this.Containers = new ObservableCollection<MessagesContainer>();
        }

        public void updateContainers()
        {
            foreach(MessagesContainer container in Containers)
            {
                container.updateAsync();
            }
        }

    }
}
