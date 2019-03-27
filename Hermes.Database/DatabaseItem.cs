using System;

namespace Hermes.Database
{
    public abstract class DatabaseItem
    {
        public Guid MessageID;
        public Guid SenderID;
        public Guid RecipientID;

        public DateTime CreatedTimestamp;
        public DateTime UpdatedTimestamp;

        public string MessageNamespace;
        public string MessageName;

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
