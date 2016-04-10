using aggregator_hub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aggregator_hub.Managers
{
    class AppManager
    {
        public MessageProviderManager MessageProviderManager { get; }
        public AppHttpClient AppHttpClient{ get; }
        public List<Stage> Stages { get; }

        private Stage currentStage;

        public AppManager()
        {
            this.AppHttpClient = new AppHttpClient();
            this.MessageProviderManager = new MessageProviderManager(new MessageProviderContext(AppHttpClient));

            // Create blank stage
            this.Stages = new List<Stage>();
            Stages.Add(new Stage(MessageProviderManager));
        }
    }
}
