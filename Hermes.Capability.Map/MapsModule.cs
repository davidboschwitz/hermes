using Autofac;
using Hermes.Database;

namespace Hermes.Capability.Map
{
    public class MapsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new MapsController(c.Resolve<DatabaseController>()))
                .As<ICapabilityController>()
                .As<MapsController>()
                .SingleInstance();
        }
    }
}
