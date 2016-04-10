using aggregator_hub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aggregator_hub.ViewModels
{
    class MessagesContainerViewModel
    {
        public List<Message> Messages { get; set; }


        public MessagesContainerViewModel()
        {
            this.Messages = new List<Message>();
            this.Messages.Add(new Message("Test", "123131233", "Test messsage content", "abcd"));
            populateListView();
        }

        public void populateListView()
        {
            
        }

    }
}
