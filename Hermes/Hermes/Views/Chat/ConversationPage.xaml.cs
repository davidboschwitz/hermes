using Hermes.Capability.Chat;
using Hermes.ViewModels.Chat;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hermes.Views.Chat
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConversationPage : ContentPage
    {
        public ConversationPage(IChatController controller, ChatPage chatPage, ChatNewConversationPage chatNewConversationPage)
        {
            InitializeComponent();

            var vm = new ConversationPageViewModel(controller, chatPage, chatNewConversationPage);
            BindingContext = vm;
            
            ConversationListView.ItemSelected += vm.SelctedItemHandler;
        }
    }
}
