using System;

namespace Hermes.Database
{
    public abstract class DatabaseItem
    {
        Guid MessageID;
        Guid SenderID;
        Guid RecipientID;

        DateTime CreatedTimestamp;
        DateTime UpdatedTimestamp;

        string MessageType;
    }
}
