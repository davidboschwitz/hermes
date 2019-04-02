using Autofac;
using Hermes.Capability;
using System.Collections.Generic;

namespace Hermes.Networking
{
    public class NetworkingModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new NetworkControllerFactory(c.Resolve<IEnumerable<ICapabilityController>>()))
                   .As<NetworkControllerFactory>()
                   .SingleInstance();

            builder.Register(c => c.Resolve<NetworkControllerFactory>().NetworkController)
                   .As<INetworkController>()
                   .SingleInstance();
        }
    }
}
