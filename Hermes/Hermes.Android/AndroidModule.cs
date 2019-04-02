using Autofac;
using Hermes.Services;

namespace Hermes.Droid
{
    public class AndroidModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new AndroidHermesSupportService())
                   .As<IHermesSupportService>()
                   .SingleInstance();
        }
    }
}
