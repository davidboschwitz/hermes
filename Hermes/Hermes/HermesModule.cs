using Autofac;
using Hermes.Database;
using Hermes.Views;

namespace Hermes
{
    public class HermesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new DatabaseModule());

            builder.Register(c => new MainPage())
                   .As<MainPage>()
                   .SingleInstance();
        }
    }
}
