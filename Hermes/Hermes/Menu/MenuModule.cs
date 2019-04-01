using Autofac;
using Hermes.Views;
using System.Collections.Generic;

namespace Hermes.Menu
{
    public class MenuModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.Register(c => new HermesMenuItem(HermesMenuItemType.News, "News Feed", c.Resolve<AboutPage>()))
                .As<HermesMenuItem>();
            builder.Register(c => new HermesMenuItem(HermesMenuItemType.Chat, "Messages", c.Resolve<AboutPage>()))
                .As<HermesMenuItem>();
            builder.Register(c => new HermesMenuItem(HermesMenuItemType.Map, "Map", c.Resolve<AboutPage>()))
                .As<HermesMenuItem>();
            builder.Register(c => new HermesMenuItem(HermesMenuItemType.About, "About Hermes", c.Resolve<AboutPage>()))
                .As<HermesMenuItem>();
        }
    }
}
