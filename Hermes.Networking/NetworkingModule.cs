using Autofac;
using Hermes.Capability;
using Hermes.Database;
using System.Collections.Generic;

namespace Hermes.Networking
{
    public class NetworkingModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new UpdateAllSequence())
                   .As<NetworkSequence>();

            builder.Register(c => new NetworkController(c.Resolve<DatabaseController>(), 
                                                        c.Resolve<IEnumerable<ICapabilityController>>(),
                                                        c.Resolve<IEnumerable<NetworkConnection>>(),
                                                        c.Resolve<IEnumerable<NetworkSequence>>()))
                   .As<NetworkController>()
                   .SingleInstance();
        }
    }
}
