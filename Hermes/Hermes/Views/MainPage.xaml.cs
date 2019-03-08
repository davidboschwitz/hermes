using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Hermes.Menu;
using Xamarin.Forms.Xaml;

namespace Hermes.Views
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : MasterDetailPage
    {

        public MainPage(MenuPage menu)
        {
            InitializeComponent();

            Master = menu;
            Detail = menu.MenuItems[0].NavigationPage;
            MasterBehavior = MasterBehavior.Popover;
        }
        
        public async Task NavigateFromMenu(NavigationPage selectedPage)
        {
            if (selectedPage != null && Detail != selectedPage)
            {
                Detail = selectedPage;

                if (Device.RuntimePlatform == Device.Android)
                    await Task.Delay(100);

                IsPresented = false;
            }
        }
    }
}
