using SQLite;
using System;

namespace Hermes.Database
{
    public abstract class DatabaseItem
    {
        [PrimaryKey]
        public Guid MessageID { get; set; }

        public Guid SenderID { get; set; }
        public Guid RecipientID { get; set; }

        public DateTime CreatedTimestamp { get; set; }
        public DateTime UpdatedTimestamp { get; set; }

        public string MessageNamespace { get; set; }
        public string MessageName { get; set; }

        public DatabaseItem()
        {
            // for sqlite autogeneration
        }

        public DatabaseItem(Guid messageID, Guid senderID, Guid recipientID, DateTime createdTimestamp, DateTime updatedTimestamp, string messageNamespace, string messageName)
        {
            MessageID = messageID;
            SenderID = senderID;
            RecipientID = recipientID;
            CreatedTimestamp = createdTimestamp;
            UpdatedTimestamp = updatedTimestamp;
            MessageNamespace = messageNamespace;
            MessageName = messageName;
        }

        public override string ToString()
        {
            return $"[{MessageID}]({SenderID}->{RecipientID})@{CreatedTimestamp}/{UpdatedTimestamp}<{MessageNamespace}.{MessageName}>";
        }
    }
}
