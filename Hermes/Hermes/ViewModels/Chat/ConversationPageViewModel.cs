using Hermes.Capability.Chat;
using Hermes.Capability.Chat.Model;
using Hermes.Views.Chat;

using System.Diagnostics;
using System.Windows.Input;

using Xamarin.Forms;

namespace Hermes.ViewModels.Chat
{
    public class ConversationPageViewModel : ChatBaseViewModel
    {
        public ICommand NewConversationCommand { get; }

        private ChatPage ChatPage { get; }
        private ChatNewConversationPage ChatNewConversationPage { get; }
        private NavigationPage NavigationChatPage { get; }

        public ChatConversation SelectedConversation
        {
            get { return null; }
            set
            {
                Controller.SelectConversation(value);
                RootPage.NavigateToPage(ChatPage);
            }
        }

        public ConversationPageViewModel(ChatController controller, ChatPage chatPage, ChatNewConversationPage chatNewConversationPage) : base(controller)
        {
            ChatPage = chatPage;
            ChatNewConversationPage = chatNewConversationPage;

            NewConversationCommand = new Command(NewConversationFunctionAsync);
        }

        private async void NewConversationFunctionAsync()
        {
            Debug.WriteLine("NewConversationFunctionAsync");
            await RootPage.NavigateToPage(ChatNewConversationPage).ConfigureAwait(false);
        }
    }
}
