using Hermes.Database;
using System;

namespace Hermes.Capability
{
    public interface ICapabilityController
    {
        void OnNotification(string messageName, Guid messageID);

        event Action<Type, DatabaseItem> SendMessage;
    }
}
