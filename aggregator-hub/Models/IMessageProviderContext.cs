﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aggregator_hub.Models
{
    interface IMessageProviderContext
    {
        AppHttpClient AppHttpClient { get; }
        MessagesContainer Container { get; }
    }
}
