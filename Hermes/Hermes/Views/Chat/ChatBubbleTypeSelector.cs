using Xamarin.Forms;
using Hermes.Capability.Chat;
using Hermes.Capability.Chat.Model;
using System.Diagnostics;
using System;

namespace Hermes.Views.Chat
{
    public class ChatBubbleTypeSelector : DataTemplateSelector
    {
        DataTemplate ChatBubbleRecieved;
        DataTemplate ChatBubbleSent;

        DataTemplate ChatBubbleImageRecieved;
        DataTemplate ChatBubbleImageSent;

        private Guid me;

        public ChatBubbleTypeSelector(IChatController controller)
        {
            me = controller.Me;

            ChatBubbleRecieved = new DataTemplate(typeof(ChatBubbleRecieved));
            ChatBubbleSent = new DataTemplate(typeof(ChatBubbleSent));

            ChatBubbleImageRecieved = new DataTemplate(typeof(ChatBubbleImageRecieved));
            ChatBubbleImageSent = new DataTemplate(typeof(ChatBubbleImageSent));
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item is ChatImageMessage imageMsg)
            {
                return imageMsg.RecipientID == me ? ChatBubbleImageRecieved : ChatBubbleImageSent;
            }
            else if (item is ChatVerificationMessage verifyMsg)
            {
                return verifyMsg.RecipientID == me ? ChatBubbleImageRecieved : ChatBubbleImageSent;
            }
            else if (item is ChatMessage chatMsg)
            {
                return chatMsg.RecipientID == me ? ChatBubbleRecieved : ChatBubbleSent;
            }
            else
            {
                return null;
            }
        }

    }
}
