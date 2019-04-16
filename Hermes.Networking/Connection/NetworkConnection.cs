using System;
using System.Threading.Tasks;

namespace Hermes.Networking
{
    public abstract class NetworkConnection
    {
        public abstract string Name { get; }
        public abstract void Open();
        public abstract void Close();

        public virtual async Task<string> ReceiveString()
        {
            await new Task(() => { });
            throw new Exception("NetworkConnection.RecieveString() must be overriden!");
        }

        public virtual async Task SendString(string s)
        {
            await new Task(() => { });
            throw new Exception("NetworkConnection.SendString() must be overriden!");
        }
    }
}
