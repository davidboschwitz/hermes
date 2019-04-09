using System;

namespace Hermes.Capability.Chat.Model
{
    public class ChatVerificationMessage : ChatMessage
    {
        public string Image { get; set; }

        public ChatVerificationMessage()
        {
        }

        public ChatVerificationMessage(Guid to, Guid from, string body, string image) : base(from, to, body, Capability.MessageNames.ChatVerificationMessage)
        {
            Image = image;
        }
    }
}
