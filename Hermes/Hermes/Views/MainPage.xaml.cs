using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Hermes.Menu;
using Xamarin.Forms.Xaml;
using System.Diagnostics;

namespace Hermes.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : MasterDetailPage
    {
        public MainPage(MenuPage menu)
        {
            InitializeComponent();

            Master = menu;
            MasterBehavior = MasterBehavior.Popover;

            Detail = menu.MenuItems[0].NavigationPage;
        }

        public async Task SetNavigationRoot(NavigationPage selectedPage)
        {
            Debug.WriteLine($"Switched to {selectedPage.RootPage.GetType().FullName}");
            if (selectedPage != null && Detail != selectedPage)
            {
                Detail = selectedPage;

                if (Device.RuntimePlatform == Device.Android)
                    await Task.Delay(100);

                IsPresented = false;
            }
        }

        public async Task NavigateToPage(ContentPage selectedPage)
        {
            Debug.WriteLine($"Switched to {selectedPage.GetType().FullName}");
            if (selectedPage != null && Detail != selectedPage)
            {
                await (Detail as NavigationPage).PushAsync(selectedPage);

                if (Device.RuntimePlatform == Device.Android)
                    await Task.Delay(100);

                IsPresented = false;
            }
        }
    }
}
