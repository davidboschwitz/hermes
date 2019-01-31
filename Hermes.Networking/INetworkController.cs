using System;
using Hermes.Networking.Messaging;

namespace Hermes.Networking
{
    public interface INetworkController
    {
        INetworkConnection CurrentConnection { get; }

        event Action<Message> MessageRecieved;
        event Action<Message> MessageSent;

        void AttachMessageHandler(MessageHandler messageHandler);
        
        void SendMessage(Message message);
    }
}
