using Hermes.Capability.Chat;
using Hermes.ViewModels.Chat;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hermes.Views.Chat
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChatNewConversationPage : ContentPage
    {
        public ChatNewConversationPage(ChatController controller, ChatPage chatPage)
        {
            InitializeComponent();
            var vm = new ChatNewConversationViewModel(controller, chatPage);
            BindingContext = vm;

            ContactsListView.ItemSelected += vm.SelectContactHandler;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            this.Animate("xx", s => Layout(new Rectangle(((-1 + s) * Width), Y, Width, Height)), 16, 250, Easing.Linear, null, null);
        }
    }
}