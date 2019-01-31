namespace Hermes.Networking.Messaging
{
    public class Message
    {
        public MessageHeader Header { get; }
        public MessagePayload Payload { get; }

        public Message(MessageHeader header, MessagePayload payload)
        {
            Header = header;
            Payload = payload;
        }
    }
}
