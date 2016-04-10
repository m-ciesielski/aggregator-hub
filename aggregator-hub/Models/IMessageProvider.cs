using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aggregator_hub.Models
{
    interface IMessageProvider

    {
        Task<List<Message>> FetchMessagesAsync(IMessageProviderContext context);
        string GetName();
    }
}
