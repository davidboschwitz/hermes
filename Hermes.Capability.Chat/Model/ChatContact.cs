using System;

namespace Hermes.Capability.Chat.Model
{
    public class ChatContact
    {
        public Guid ID { get; set; }
        public string Name { get; set; }

        public ChatContact(Guid id, string name)
        {
            ID = id;
            Name = name;
        }

        public ChatContact() { }
    }
}
