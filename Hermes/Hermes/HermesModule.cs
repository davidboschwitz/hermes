using Autofac;

using Hermes.Capability.Chat;
using Hermes.Capability.Map;
using Hermes.Database;
using Hermes.Menu;
using Hermes.Networking;
using Hermes.Views;

using System.Collections.Generic;
using Xamarin.Forms;

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
            builder.RegisterModule(new MapsModule());
            builder.RegisterModule(new ChatModule());

            //view modules
            builder.RegisterModule(new MenuModule());
            builder.RegisterModule(new ViewsModule());
        }
    }
}
