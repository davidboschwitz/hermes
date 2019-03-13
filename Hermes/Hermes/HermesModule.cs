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
            builder.RegisterModule(new MenuModule());
            builder.RegisterModule(new ViewsModule());
        }
    }
}
