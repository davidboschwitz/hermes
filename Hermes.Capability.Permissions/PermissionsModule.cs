using Autofac;
using Hermes.Database;

namespace Hermes.Capability.Permissions
{
    public class PermissionsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new PermissionsController(c.Resolve<DatabaseController>()))
                   .As<ICapabilityController>()
                   .As<PermissionsController>()
                   .SingleInstance();
        }
    }
}
