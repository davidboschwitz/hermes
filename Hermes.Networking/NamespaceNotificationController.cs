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

        public event Action<string, Guid> Notify;

        public void FireNotify(string messageName, Guid messageID)
        {
            Notify.Invoke(messageName, messageID);
        }
    }
}
