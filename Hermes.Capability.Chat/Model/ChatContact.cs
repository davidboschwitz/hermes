using Hermes.Database;
using System;

namespace Hermes.Capability.Chat.Model
{
    public class ChatContact : DatabaseItem
    {
        [SQLite.Ignore]
        public Guid ID
        {
            get
            {
                return Guid.Parse(MessageID.ToString());
            }
        }
        public string Name { get; set; }

        public ChatContact(Guid id, string name) : base(id, new Guid(), new Guid(), DateTime.Now, DateTime.Now, Capability.Namespace, Capability.MessageNames.ChatContact)
        {
            Name = name;
        }

        public ChatContact() { }
    }
}
