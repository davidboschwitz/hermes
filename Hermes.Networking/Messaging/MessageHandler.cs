using System.Collections.Generic;

namespace Hermes.Networking.Messaging
{
    public abstract class MessageHandler
    {
        public abstract string Namespace { get; }
        public abstract ISet<string> Names { get; }

        public MessageHandler()
        {
        }

        public abstract void RecieveMessage(Message message); // throws UnhandledMessageException;
        public abstract void SendMessage(Message message);
    }
}
