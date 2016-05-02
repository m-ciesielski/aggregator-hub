using aggregator_hub.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;

namespace aggregator_hub.ViewModels
{
    class MessagesContainerViewModel
    {
        public MessagesContainer Model { get; set; }
        // Messages collection is exposed as a property just for convenience
        public ObservableCollection<Message> Messages { get; set; }
        public ICommand Update
        {
            get
            {
                return new CommandHandler(() => this.UpdateAction());
            }
        }


        public MessagesContainerViewModel(MessagesContainer model)
        {
            this.Model = model;
            this.Messages = model.Messages;
        }

        private void UpdateAction()
        {
            Model.updateAsync();
        }

    }
}
