using Autofac;

namespace Hermes.Views
{
    public class ViewsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new AboutPage())
                .As<AboutPage>()
                .SingleInstance();
        }
    }
}
