using Hermes.Capability.Chat;
using Hermes.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hermes.Views.Chat
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChatNewConversationPage : ContentPage
    {
        public ChatNewConversationPage(IChatController controller, ChatPage chatPage)
        {
            InitializeComponent();
            var vm = new ChatNewConversationViewModel(controller, chatPage);
            BindingContext = vm;

            ContactsListView.ItemSelected += vm.SelectContactHandler;
        }
    }
}