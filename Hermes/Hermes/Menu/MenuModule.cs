using Autofac;
using Hermes.Pages;
using Hermes.Views;
using Hermes.Views.Chat;
using static Hermes.Capability.Permissions.PermissionsController;

namespace Hermes.Menu
{
    public class MenuModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new HermesMenuItem("News", c.Resolve<NewsPage>()))
                .As<HermesMenuItem>();
            builder.Register(c => new HermesMenuItem("Chat", c.Resolve<ConversationPage>()))
                .As<HermesMenuItem>();
            builder.Register(c => new HermesMenuItem("Map", c.Resolve<MapPage>()))
                .As<HermesMenuItem>();
            builder.Register(c => new HermesMenuItem("Points of Interest", c.Resolve<PinScrollPage>()))
                .As<HermesMenuItem>();
            builder.Register(c => new HermesMenuItem("Maps - Add Pin", c.Resolve<PinInfoPage>(), Level.ADMIN))
                .As<HermesMenuItem>();
            builder.Register(c => new HermesMenuItem("News - Add Report", c.Resolve<NewsAdminPage>(), Level.ADMIN))
                .As<HermesMenuItem>();
            builder.Register(c => new HermesMenuItem("About", c.Resolve<AboutPage>()))
                .As<HermesMenuItem>();
            builder.Register(c => new HermesMenuItem("Net", c.Resolve<NetSyncPageWow>()))
                .As<HermesMenuItem>();
        }
    }
}
