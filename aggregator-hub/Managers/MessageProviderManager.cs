using aggregator_hub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aggregator_hub.Managers
{
    class MessageProviderManager
    {
        public List<IMessageProvider> Providers { get; set; }

        public IMessageProviderContext Context { get; }

        public MessageProviderManager(IMessageProviderContext context)
        {
            this.Context = context;
        }

        public void LoadMessageProviders()
        {

        }

    }
}
