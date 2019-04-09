using System;
using Hermes.Database;

namespace Hermes.Capability.Chat
{
    public class ChatMessage : DatabaseItem
    {
        public string Body { get; set; }

        public ChatMessage()
        {
        }

        public ChatMessage(Guid to, Guid from, string body) : base(Guid.NewGuid(), from, to, DateTime.Now, DateTime.Now, Capability.Namespace, Capability.MessageNames.ChatMessage)
        {
            Body = body;
        }

        internal ChatMessage(Guid to, Guid from, string body, string messageName) : base(Guid.NewGuid(), from, to, DateTime.Now, DateTime.Now, Capability.Namespace, messageName)
        {
            Body = body;
        }
    }
}
