using Autofac;
using Hermes.Services;

namespace Hermes.UWP
{
    public class UWPModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new UWPHermesSupportService())
                   .As<IHermesSupportService>()
                   .SingleInstance();

            builder.Register(c => new UWPToastService())
                   .As<IHermesToastService>()
                   .SingleInstance();
        }
    }
}
