using System;
using Hermes.Database;
using Hermes.Networking.Messaging;

namespace Hermes.Networking
{
    public interface INetworkController
    {
        INetworkConnection CurrentConnection { get; }

        event Action<DatabaseItem> MessageRecieved;
        event Action<DatabaseItem> MessageSent;

        void AttachMessageHandler(MessageHandler messageHandler);
        
        void SendMessage(DatabaseItem message);
    }
}
