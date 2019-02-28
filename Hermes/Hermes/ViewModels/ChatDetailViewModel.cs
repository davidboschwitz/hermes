using System;

using Hermes.Models;

namespace Hermes.ViewModels
{
    public class ChatDetailViewModel { 

        public Item Item { get; set; }
        public String Title { get; set; }
        public ChatDetailViewModel(Item item = null)
        {
            Title = item?.Text;
            Item = item;
        }
    }
}
