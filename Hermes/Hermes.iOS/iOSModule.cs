using Autofac;
using Hermes.Services;

namespace Hermes.iOS
{
    public class iOSModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new iOSHermesSupportService())
                   .As<IHermesSupportService>()
                   .SingleInstance();
        }
    }
}
