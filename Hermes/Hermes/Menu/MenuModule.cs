using Autofac;
using Hermes.Pages;
using Hermes.Views;

namespace Hermes.Menu
{
    public class MenuModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new HermesMenuItem(HermesMenuItemType.News, "News", c.Resolve<AboutPage>()))
                .As<HermesMenuItem>();
            builder.Register(c => new HermesMenuItem(HermesMenuItemType.Chat, "Chat", c.Resolve<AboutPage>()))
                .As<HermesMenuItem>();
            builder.Register(c => new HermesMenuItem(HermesMenuItemType.Map, "Map", c.Resolve<MapPage>()))
                .As<HermesMenuItem>();
            builder.Register(c => new HermesMenuItem(HermesMenuItemType.Map, "Admin Map", c.Resolve<PinInfoPage>()))
                .As<HermesMenuItem>();
            builder.Register(c => new HermesMenuItem(HermesMenuItemType.About, "About", c.Resolve<AboutPage>()))
                .As<HermesMenuItem>();
        }
    }
}
