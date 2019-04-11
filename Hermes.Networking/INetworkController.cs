using System;
using System.Collections.Generic;
using Hermes.Database;

namespace Hermes.Networking
{
    public interface INetworkController
    {
        INetworkConnection CurrentConnection { get; }

        List<TableSyncController> TableSyncControllers { get; }
        Dictionary<string, NamespaceNotificationController> NamespaceNotificationControllers { get; }

        void SendMessage(Type type, DatabaseItem obj);

        //event Action<DatabaseItem> MessageReceived;
        //event Action<DatabaseItem> MessageSent;

        //void AttachMessageHandler(MessageHandler messageHandler);

        //void SendMessage(DatabaseItem message);
    }
}
