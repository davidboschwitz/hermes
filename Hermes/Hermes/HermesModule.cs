using Autofac;
using Hermes.Database;
using Hermes.Menu;
using Hermes.Views;
using System.Collections.Generic;

namespace Hermes
{
    public class HermesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new DatabaseModule());
            builder.RegisterModule(new ViewsModule());
            builder.RegisterModule(new MenuModule());

            builder.Register(c => new MainPage(c.Resolve<MenuPage>()))
                   .As<MainPage>()
                   .SingleInstance();
        }
    }
}
