using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aggregator_hub.Models
{
    class Message
    {
        public string Header { get; set; }
        public string Timestamp { get; set; }
        public string Content { get; set; }
        public string Link { get; set; }

        public Message()
        {

        }

        public Message(string header, string timestamp, string content, string link)
        {
            this.Header = header;
            this.Timestamp = timestamp;
            this.Content = content;
            this.Link = link;
        }
    }
}
