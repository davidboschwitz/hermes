using Autofac;

using Hermes.Database;
using Hermes.Networking;
using Hermes.Capability.News;
using Hermes.Capability.Map;
using Hermes.Capability.Chat;

namespace Hermes.Server
{
    public class ServerModule : Module
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

            builder.Register(c => new SocketListener(c.Resolve<NetworkController>()))
                   .As<SocketListener>()
                   .SingleInstance();
        }
    }
}
