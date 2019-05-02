using Autofac;
using Hermes.Capability.Chat;
using Hermes.Capability.Map;
using Hermes.Capability.News;
using Hermes.Capability.Permissions;
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
            builder.RegisterModule(new MapsModule());
            builder.RegisterModule(new ChatModule());
            builder.RegisterModule(new PermissionsModule());

            //view modules
            builder.RegisterModule(new MenuModule());
            builder.RegisterModule(new ViewsModule());
        }
    }
}
