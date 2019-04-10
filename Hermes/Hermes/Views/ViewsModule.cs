using Autofac;
using Hermes.Capability.Chat;
using Hermes.Menu;
using Hermes.Pages;
using Hermes.Views.Chat;
using System.Collections.Generic;

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

            builder.Register(c => new MapPage())
                   .As<MapPage>()
                   .SingleInstance();

            builder.Register(c => new PinInfoPage())
                   .As<PinInfoPage>()
                   .SingleInstance();

            builder.Register(c => new PinScrollPage())
                   .As<PinScrollPage>()
                   .SingleInstance();

            builder.Register(c => new ChatPage(c.Resolve<IChatController>()))
                   .As<ChatPage>()
                   .SingleInstance();

            builder.Register(c => new ConversationPage(c.Resolve<IChatController>(), c.Resolve<ChatPage>()))
                   .As<ConversationPage>()
                   .SingleInstance();

            builder.Register(c => new ChatVerificationCreatorPage(c.Resolve<IChatController>()))
                   .As<ChatVerificationCreatorPage>()
                   .SingleInstance();

            builder.Register(c => new MainPage(c.Resolve<MenuPage>()))
                   .As<MainPage>()
                   .SingleInstance();
        }
    }
}
