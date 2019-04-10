using Hermes.Capability.Chat;
using Hermes.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hermes.Views.Chat
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddContactPage : ContentPage
    {
        public AddContactPage(IChatController controller)
        {
            InitializeComponent();

            BindingContext = new AddContactViewModel(controller);
        }
    }
}