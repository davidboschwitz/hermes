using Hermes.Database;
using System;
using System.ComponentModel;

namespace Hermes.Capability
{
    public interface ICapabilityController : INotifyPropertyChanged
    {
        void OnNotification(string messageNamespace, string messageName, Guid messageID);

        event Action<Type, DatabaseItem> SendMessage;
    }
}
