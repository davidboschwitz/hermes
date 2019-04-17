using Hermes.Menu;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hermes.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : ContentPage
    {
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }
        public List<HermesMenuItem> MenuItems = new List<HermesMenuItem>();

        public MenuPage(IEnumerable<HermesMenuItem> menuItems)
        {
            InitializeComponent();

            foreach (var menuItem in menuItems)
            {
                MenuItems.Add(menuItem);
            }

            ListViewMenu.ItemsSource = MenuItems;

            ListViewMenu.SelectedItem = MenuItems[0];

            ListViewMenu.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                    return;

                var navigationPage = ((HermesMenuItem)e.SelectedItem).NavigationPage;
                await RootPage.SetNavigationRoot(navigationPage);
            };
        }
    }
}
