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
    }
}
