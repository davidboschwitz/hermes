using System;

namespace Hermes.Capability.Chat.Model
{
    public class ChatImageMessage : ChatMessage
    {
        public string Image { get; set; }

        public ChatImageMessage(Guid to, Guid from, string body, string image) : base(to, from, body, Capability.MessageNames.ChatImageMessage)
        {
            Image = image;
        }

        public ChatImageMessage() { }
    }
}
