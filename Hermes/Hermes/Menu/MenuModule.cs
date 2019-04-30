using Autofac;
using Hermes.Pages;
using Hermes.Views;
using Hermes.Views.Chat;

namespace Hermes.Menu
{
    public class MenuModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new HermesMenuItem(Resources.News, c.Resolve<NewsPage>()))
                .As<HermesMenuItem>();
            builder.Register(c => new HermesMenuItem(Resources.Chat, c.Resolve<ConversationPage>()))
                .As<HermesMenuItem>();
            builder.Register(c => new HermesMenuItem(Resources.ChatVerify, c.Resolve<ChatVerificationCreatorPage>()))
                .As<HermesMenuItem>();
            builder.Register(c => new HermesMenuItem(Resources.Map, c.Resolve<MapPage>()))
                .As<HermesMenuItem>();
            builder.Register(c => new HermesMenuItem(Resources.MapInfo, c.Resolve<PinInfoPage>()))
                .As<HermesMenuItem>();
            builder.Register(c => new HermesMenuItem(Resources.MapPinScrollPage, c.Resolve<PinScrollPage>()))
                .As<HermesMenuItem>();
            builder.Register(c => new HermesMenuItem(Resources.About, c.Resolve<AboutPage>()))
                .As<HermesMenuItem>();
            builder.Register(c => new HermesMenuItem(Resources.Net, c.Resolve<NetSyncPageWow>()))
                .As<HermesMenuItem>();
        }
    }
}
