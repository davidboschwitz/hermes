using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Hermes.Views;
using Autofac;
using Hermes.Networking;
using System;
using System.Diagnostics;
using Hermes.Services;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Hermes
{
    public partial class App : Application
    {
        IContainer Container;
        NetworkController NetworkController;

        public static double ScreenWidth;
        public static double ScreenHeight;

        public App() : this(new ContainerBuilder()) { }

        public App(ContainerBuilder builder)
        {
            InitializeComponent();

            builder.Register<Application>(c => this);
            builder.RegisterModule(new HermesModule());
            Container = builder.Build();

            var networkController = Container.Resolve<NetworkController>();
            NetworkController = networkController;

            //Container.Resolve<IHermesSupportService>().HermesIdentifier();

            var mainPage = Container.Resolve<MainPage>();
            MainPage = mainPage;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }


        public void SetWidth(double w)
        {
            ScreenWidth = w;
        }

        public void SetHeight(double h)
        {
            ScreenHeight = h;
        }

        public double GetWidth()
        {
            return ScreenWidth;
        }

        public double GetHeight()
        {
            return ScreenHeight;
        }
    }
}
