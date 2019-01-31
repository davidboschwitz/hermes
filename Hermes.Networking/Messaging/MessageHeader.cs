using System;

namespace Hermes.Networking.Messaging
{
    public class MessageHeader
    {
        public readonly string Namespace;
        public readonly string Name;
        public readonly DateTime Timestamp;

        public MessageHeader(string @namespace, string name, DateTime timestamp)
        {
            Namespace = @namespace;
            Name = name;
            Timestamp = timestamp;
        }
    }
}
