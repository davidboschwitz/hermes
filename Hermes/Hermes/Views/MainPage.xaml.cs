using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Hermes.Menu;
using Xamarin.Forms.Xaml;
using System.Diagnostics;
using Hermes.Database;

namespace Hermes.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : MasterDetailPage
    {
        public MainPage(MenuPage menu, DatabaseController db)
        {
            InitializeComponent();

            Master = menu;
            MasterBehavior = MasterBehavior.Popover;

            if (db.GetProperty("OOBE") == null)
            {
                Detail = new OOBEPage(db, menu.MenuItems[0].NavigationPage);
            }
            else
            {
                Detail = menu.MenuItems[0].NavigationPage;
            }
        }

        public void OpenMenu()
        {
            IsPresented = true;
        }

        public async void CloseMenu()
        {
            if (Device.RuntimePlatform == Device.Android)
                await Task.Delay(100);

            IsPresented = false;
        }

        public async Task SetNavigationRoot(NavigationPage selectedPage)
        {
            if (selectedPage != null && Detail != selectedPage)
            {
                Debug.WriteLine($"Switched to {selectedPage.RootPage.GetType().FullName}");
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

        public async Task NavigateToPageReplaceLast(ContentPage selectedPage)
        {
            Debug.WriteLine($"Switched to {selectedPage.GetType().FullName}");
            if (selectedPage != null && Detail != selectedPage)
            {
                var navPage = (Detail as NavigationPage);
                //navPage.Animate("asdf", Animation)
                await navPage.PopAsync();
                await navPage.PushAsync(selectedPage);

                if (Device.RuntimePlatform == Device.Android)
                    await Task.Delay(100);

                IsPresented = false;
            }
        }

        public async Task NavigatePop()
        {
            var navPage = (Detail as NavigationPage);
            if (navPage != null)
            {
                await navPage.PopAsync();

                if (Device.RuntimePlatform == Device.Android)
                    await Task.Delay(100);

                IsPresented = false;
            }
        }

    }
}
