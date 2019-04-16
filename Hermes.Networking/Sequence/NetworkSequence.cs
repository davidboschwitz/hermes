using Hermes.Database;

using System;
using System.Threading.Tasks;

namespace Hermes.Networking
{
    public abstract class NetworkSequence
    {
        public abstract string SequenceID { get; }

        public virtual async Task Run(NetworkController controller, DatabaseController db, NetworkConnection net)
        {
            await new Task(() => { });
            throw new Exception("NetworkSequence.Run() must be overriden!");
        }
    }
}
