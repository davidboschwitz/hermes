using Hermes.Capability.Chat;
using Hermes.Capability.Chat.Model;
using Hermes.ViewModels.Chat;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hermes.Views.Chat
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConversationPage : ContentPage
    {
        public ConversationPage(ChatController controller, ChatPage chatPage, ChatNewConversationPage chatNewConversationPage)
        {
            InitializeComponent();

            var vm = new ConversationPageViewModel(controller, chatPage, chatNewConversationPage);
            BindingContext = vm;
            
            //ConversationListView.ItemSelected += async (e, a) => {
            //    controller.SelectConversation(a.SelectedItem as ChatContact);
            //    await Task.Delay(1000);
            //    ConversationListView.SelectedItem = null;
            //};
        }
    }
}
