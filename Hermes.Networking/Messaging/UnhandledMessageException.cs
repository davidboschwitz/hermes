using System;

namespace Hermes.Networking.Messaging
{
    public class UnhandledMessageException : Exception
    {
        public UnhandledMessageException(string message) : base(message)
        {
        }

        public UnhandledMessageException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}