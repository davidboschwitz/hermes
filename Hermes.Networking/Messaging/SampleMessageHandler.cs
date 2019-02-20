using System.Collections.Generic;

namespace Hermes.Networking.Messaging
{
    public class SampleMessageHandler : MessageHandler
    {
        public override string Namespace => "SampleChat";

        private const string MESSAGE_NAME_TEXT_MESSAGE = "TextMessage";
        private const string MESSAGE_NAME_READ_RECIEPT = "ReadReciept";
        private const string MESSAGE_NAME_ERROR = "Error";

        public override ISet<string> Names => new HashSet<string>() { MESSAGE_NAME_TEXT_MESSAGE, MESSAGE_NAME_READ_RECIEPT, MESSAGE_NAME_ERROR };

        private INetworkController NetworkController;

        public SampleMessageHandler(INetworkController networkController) : base()
        {
            NetworkController = networkController;
        }

        public override void RecieveMessage(Message message)
        {
            var header = message?.Header;
            if (!Namespace.Equals(header?.Namespace))
            {
                throw new UnhandledMessageException($"{header.Namespace}.{header.Name} cannot be handled by SampleMessageHandler.");
            }
            else if (MESSAGE_NAME_TEXT_MESSAGE.Equals(header.Name))
            {
                var payload = new TextMessagePayload(message.RawPayload);
            }
            else if (MESSAGE_NAME_READ_RECIEPT.Equals(header.Name))
            {

            }
            else if (MESSAGE_NAME_ERROR.Equals(header.Name))
            {
                 
            }
            else
            {
                throw new UnhandledMessageException($"{header.Namespace}.{header.Name} cannot be handled by SampleMessageHandler.");
            }
        }

        public override void SendMessage(Message message)
        {
            NetworkController.SendMessage(message);
        }
    }
}
