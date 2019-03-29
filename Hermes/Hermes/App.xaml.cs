using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Hermes.Views;
using Autofac;
using Hermes.Networking;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Hermes
{
    public partial class App : Application
    {

        IContainer Container;
        INetworkController NetworkController;

        public App()
        {
            InitializeComponent();

            var builder = new ContainerBuilder();
            builder.RegisterModule(new HermesModule());
            Container = builder.Build();

            var networkController = Container.Resolve<NetworkControllerFactory>().NetworkController;
            NetworkController = networkController;

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
    }
}
