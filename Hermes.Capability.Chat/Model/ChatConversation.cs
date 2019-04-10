using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace Hermes.Capability.Chat.Model
{
    public class ChatConversation
    {
        public ChatContact Contact { get; set; }
        public Guid Other { get; set; }
        public ObservableCollection<ChatMessage> Messages { get; set; }

        public ChatMessage LastMessage { get
            {
                if (Messages.Count == 0)
                    return null;
                return Messages.Last();
            } }
        public DateTime? LastTimestamp => LastMessage?.CreatedTimestamp;
        public string LastBody => LastMessage?.Body;

        public ChatConversation(ChatContact contact)
        {
            Messages = new ObservableCollection<ChatMessage>();
            Contact = contact;
            Other = contact.ID;

            Debug.WriteLine(this);
        }

        public void Sort()
        {
            Messages = new ObservableCollection<ChatMessage>(Messages.OrderBy(msg => msg.CreatedTimestamp));
        }

        public override string ToString()
        {
            return $"Conversation({Contact.Name}, {Messages.Count} messages, {LastTimestamp})";
        }
    }
}
