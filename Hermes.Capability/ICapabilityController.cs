using Hermes.Networking;

namespace Hermes.Capability
{
    public interface ICapabilityController
    {
        MessageHandler MessageHandler { get; }
    }
}
