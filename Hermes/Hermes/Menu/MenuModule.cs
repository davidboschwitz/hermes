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
            builder.Register(c => new HermesMenuItem("News", c.Resolve<NewsPage>()))
                .As<HermesMenuItem>();
            builder.Register(c => new HermesMenuItem("Chat", c.Resolve<ConversationPage>()))
                .As<HermesMenuItem>();
            builder.Register(c => new HermesMenuItem("Chat/Verify", c.Resolve<ChatVerificationCreatorPage>()))
                .As<HermesMenuItem>();
            builder.Register(c => new HermesMenuItem("Map", c.Resolve<MapPage>()))
                .As<HermesMenuItem>();
            builder.Register(c => new HermesMenuItem("Map/PinInfoPage", c.Resolve<PinInfoPage>()))
                .As<HermesMenuItem>();
            builder.Register(c => new HermesMenuItem("Map/PinScrollPage", c.Resolve<PinScrollPage>()))
                .As<HermesMenuItem>();
            builder.Register(c => new HermesMenuItem("About", c.Resolve<AboutPage>()))
                .As<HermesMenuItem>();
        }
    }
}
