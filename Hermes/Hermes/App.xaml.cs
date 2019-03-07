using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Hermes.Views;
using Autofac;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Hermes
{
    public partial class App : Application
    {

        IContainer Container;

        public App()
        {
            InitializeComponent();

            var builder = new ContainerBuilder();
            builder.RegisterModule(new HermesModule());
            Container = builder.Build();

            MainPage = Container.Resolve<MainPage>();
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
    }
}
