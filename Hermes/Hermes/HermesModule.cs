using Autofac;
using Hermes.Capability.News;
using Hermes.Database;
using Hermes.Menu;
using Hermes.Networking;
using Hermes.Views;

namespace Hermes
{
    public class HermesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //core modules
            builder.RegisterModule(new DatabaseModule());
            builder.RegisterModule(new NetworkingModule());

            //capability modules
            builder.RegisterModule(new NewsModule());

            //view modules
            builder.RegisterModule(new MenuModule());
            builder.RegisterModule(new ViewsModule());
        }
    }
}
