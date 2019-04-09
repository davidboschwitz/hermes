using Hermes.Database;
using System;

namespace Hermes.Networking
{
    public class NamespaceNotificationController
    {
        public string Namespace { get; }

        public NamespaceNotificationController(string @namespace)
        {
            Namespace = @namespace;
        }

        public event Action<string, string, Guid> Notify;

        public void FireNotify(string messageNamespace, string messageName, Guid messageID)
        {
            Notify?.Invoke(messageNamespace, messageName, messageID);
        }
    }
}
