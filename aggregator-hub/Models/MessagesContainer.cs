﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aggregator_hub.Models
{
    class MessagesContainer
    {
        public string Name { get; set; }
        public ObservableCollection<Message> Messages { get; set; }
        public IMessageProvider MessageProvider { get; set; }

        protected IMessageProviderContext MessageProviderContext;

        public MessagesContainer(IMessageProviderContext messageProviderContext)
        {
            this.MessageProviderContext = messageProviderContext;
            this.Messages = new ObservableCollection<Message>();
        }

        public async void updateAsync()
        {
            List<Message> newMessages = await MessageProvider.FetchMessagesAsync(MessageProviderContext);
            foreach (var message in newMessages)
            {
                System.Diagnostics.Debug.WriteLine(message);
                if (!Messages.Contains(message))
                {
                    Messages.Add(message);
                }
                    
            }

            
        }
    }
}
