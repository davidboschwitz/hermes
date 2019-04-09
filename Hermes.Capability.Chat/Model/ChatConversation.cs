using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace Hermes.Capability.Chat.Model
{
    public class ChatConversation
    {
        public string Name { get; set; }
        public string ImageBase64 { get; set; }
        public Guid Other { get; set; }
        public ObservableCollection<ChatMessage> Messages { get; set; }

        public ChatMessage LastMessage => Messages.Last();
        public DateTime LastTimestamp => LastMessage.CreatedTimestamp;
        public string LastBody => LastMessage.Body;

        public ChatConversation(Guid me, Guid other)
        {
            Messages = new ObservableCollection<ChatMessage>()
            {
                new ChatMessage(me, other, "message to me 1"),
                new ChatMessage(other, me, "message from me 1"),
                new ChatMessage(me, other, "message to me 2"),
                new ChatMessage(other, me, "message from me 2"),
                new ChatMessage(me, other, "message to me 3"),
                new ChatMessage(other, me, "message from me 3"),
            };

            Other = other;
            Name = "David";// other.ToString();

            Debug.WriteLine(this);
        }

        public void Sort()
        {
            Messages = new ObservableCollection<ChatMessage>(Messages.OrderBy(msg => msg.CreatedTimestamp));
        }
        
        public override string ToString()
        {
            return $"Conversation({Name}, {Messages.Count} messages, {LastTimestamp})";
        }
    }
}
