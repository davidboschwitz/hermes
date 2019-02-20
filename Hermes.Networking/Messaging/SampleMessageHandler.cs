using Hermes.Database;
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

        public override void RecieveMessage(DatabaseItem message)
        {
            if (!Namespace.Equals(message?.MessageNamespace))
            {
                throw new UnhandledMessageException($"{message.MessageNamespace}.{message.MessageName} cannot be handled by SampleMessageHandler.");
            }
            else if (MESSAGE_NAME_TEXT_MESSAGE.Equals(message.MessageName))
            {

            }
            else if (MESSAGE_NAME_READ_RECIEPT.Equals(message.MessageName))
            {

            }
            else if (MESSAGE_NAME_ERROR.Equals(message.MessageName))
            {
                 
            }
            else
            {
                throw new UnhandledMessageException($"{message.MessageNamespace}.{message.MessageName} cannot be handled by SampleMessageHandler.");
            }
        }

        public override void SendMessage(DatabaseItem message)
        {
            NetworkController.SendMessage(message);
        }
    }
}
