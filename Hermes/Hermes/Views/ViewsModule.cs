using Autofac;
using Hermes.Capability.News;
using Hermes.Capability.Chat;
using Hermes.Capability.Map;
using Hermes.Menu;
using Hermes.Pages;
using Hermes.Views.Chat;
using System.Collections.Generic;
using Hermes.Networking;
using Hermes.Database;
using Hermes.Services;

namespace Hermes.Views
{
    public class ViewsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new MenuPage(menuItems: c.Resolve<IEnumerable<HermesMenuItem>>()))
                   .As<MenuPage>()
                   .SingleInstance();

            builder.Register(c => new AboutPage())
                   .As<AboutPage>()
                   .SingleInstance();

            builder.Register(c => new NewsPage(c.Resolve<NewsController>()))
                   .As<NewsPage>()
                   .SingleInstance();

            builder.Register(c => new NewsAdminPage(c.Resolve<NewsController>()))
                   .As<NewsAdminPage>()
                   .SingleInstance();

            builder.Register(c => new MapPage(c.Resolve<MapsController>()))
                   .As<MapPage>()
                   .SingleInstance();

            builder.Register(c => new PinInfoPage(c.Resolve<MapsController>()))
                   .As<PinInfoPage>()
                   .SingleInstance();

            builder.Register(c => new PinScrollPage(c.Resolve<MapsController>()))
                   .As<PinScrollPage>()
                   .SingleInstance();

            builder.Register(c => new ChatPage(c.Resolve<ChatController>()))
                   .As<ChatPage>()
                   .SingleInstance();

            builder.Register(c => new AddContactPage(c.Resolve<ChatController>()))
                   .As<AddContactPage>()
                   .SingleInstance();

            builder.Register(c => new ChatNewConversationPage(c.Resolve<ChatController>(), c.Resolve<ChatPage>()))
                   .As<ChatNewConversationPage>()
                   .SingleInstance();

            builder.Register(c => new ConversationPage(c.Resolve<ChatController>(), c.Resolve<ChatPage>(), c.Resolve<ChatNewConversationPage>()))
                   .As<ConversationPage>()
                   .SingleInstance();

            builder.Register(c => new ChatVerificationCreatorPage(c.Resolve<ChatController>()))
                   .As<ChatVerificationCreatorPage>()
                   .SingleInstance();

            builder.Register(c => new NetSyncPageWow(c.Resolve<NetworkController>(), c.ResolveOptional<IHermesBluetoothService>()))
                   .As<NetSyncPageWow>()
                   .SingleInstance();

            builder.Register(c => new MainPage(c.Resolve<MenuPage>(), c.Resolve<DatabaseController>()))
                   .As<MainPage>()
                   .SingleInstance();
        }
    }
}
