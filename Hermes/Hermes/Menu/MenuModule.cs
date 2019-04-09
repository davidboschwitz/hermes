using Autofac;
using Hermes.Views;
using Hermes.Views.Chat;

namespace Hermes.Menu
{
    public class MenuModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new HermesMenuItem(HermesMenuItemType.News, "News", c.Resolve<AboutPage>()))
                .As<HermesMenuItem>();
            builder.Register(c => new HermesMenuItem(HermesMenuItemType.Chat, "Chat", c.Resolve<ConversationPage>()))
                .As<HermesMenuItem>();
            builder.Register(c => new HermesMenuItem(HermesMenuItemType.Map, "Map", c.Resolve<AboutPage>()))
                .As<HermesMenuItem>();
            builder.Register(c => new HermesMenuItem(HermesMenuItemType.About, "About", c.Resolve<AboutPage>()))
                .As<HermesMenuItem>();
        }
    }
}
