using Xamarin.Forms;
using Hermes.Capability.Chat;
using System.Diagnostics;
using System;

namespace Hermes.Views.Chat
{
    public class ChatBubbleTypeSelector : DataTemplateSelector
    {
        DataTemplate ChatBubbleRecieved;
        DataTemplate ChatBubbleSent;

        private Guid me = new Guid("89c50f2b-83ce-4b05-9c9c-b50c3067e7e1");
        //private IChatController Controller;

        public ChatBubbleTypeSelector()//IChatController controller)
        {
            //Controller = controller;

            ChatBubbleRecieved = new DataTemplate(typeof(ChatBubbleRecieved));
            ChatBubbleSent = new DataTemplate(typeof(ChatBubbleSent));
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var msg = item as ChatMessage;
            if (msg == null)
                return null;

            if (msg.RecipientID == me)//Controller.Me)
            {
                Debug.WriteLine($"R:{msg.Body}");
                return ChatBubbleRecieved;
            }
            else
            {
                Debug.WriteLine($"S:{msg.Body}");
                return ChatBubbleSent;
            }
        }

    }
}
