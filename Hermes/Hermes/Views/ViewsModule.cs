﻿using Autofac;
using Hermes.Menu;
using Hermes.Pages;
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

            builder.Register(c => new MainPage(c.Resolve<MenuPage>()))
                   .As<MainPage>()
                   .SingleInstance();
        }
    }
}
