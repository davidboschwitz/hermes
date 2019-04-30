using Hermes.Database;
using Hermes.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hermes.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OOBEPage : ContentPage
    {
        public OOBEPage(DatabaseController databaseController, NavigationPage navigationPage)
        {
            InitializeComponent();

            var vm = new OOBEViewModel(databaseController, navigationPage);
            BindingContext = vm;

            OOBEInputName.Completed += (a, b) => { OOBEInputURL.Focus(); };
            OOBEInputURL.Completed += (a, b) => { vm.CreateContactCommand.Execute(a); };
        }
    }
}
