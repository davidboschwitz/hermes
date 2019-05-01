using Hermes.Capability.Chat;
using Hermes.Capability.Chat.Model;
using System;
using Xamarin.Forms;

namespace Hermes.Views.Chat
{
    public class ChatBubbleTypeSelector : DataTemplateSelector
    {
        DataTemplate ChatBubbleReceived;
        DataTemplate ChatBubbleSent;

        DataTemplate ChatBubbleImageReceived;
        DataTemplate ChatBubbleImageSent;

        DataTemplate ChatBubbleVerificationReceived;

        private Guid me;

        public ChatBubbleTypeSelector(ChatController controller)
        {
            me = controller.Me;

            ChatBubbleReceived = new DataTemplate(typeof(ChatBubbleReceived));
            ChatBubbleSent = new DataTemplate(typeof(ChatBubbleSent));

            ChatBubbleImageReceived = new DataTemplate(typeof(ChatBubbleImageReceived));
            ChatBubbleImageSent = new DataTemplate(typeof(ChatBubbleImageSent));


            ChatBubbleVerificationReceived = new DataTemplate(() => { return new ChatBubbleVerificationReceived(controller); });
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item is ChatImageMessage imageMsg)
            {
                return imageMsg.RecipientID == me ? ChatBubbleImageReceived : ChatBubbleImageSent;
            }
            else if (item is ChatVerificationMessage verifyMsg)
            {
                return verifyMsg.RecipientID == me ? ChatBubbleVerificationReceived : ChatBubbleImageSent;
            }
            else if (item is ChatMessage chatMsg)
            {
                return chatMsg.RecipientID == me ? ChatBubbleReceived : ChatBubbleSent;
            }
            else
            {
                return null;
            }
        }

    }
}
