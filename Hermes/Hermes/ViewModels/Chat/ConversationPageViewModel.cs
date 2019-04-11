using Hermes.Capability.Chat;
using Hermes.Capability.Chat.Model;
using Hermes.Views.Chat;

using System;
using System.Diagnostics;
using System.Windows.Input;

using Xamarin.Forms;

namespace Hermes.ViewModels.Chat
{
    public class ConversationPageViewModel : ChatBaseViewModel
    {
        public Guid Me => new Guid("89c50f2b-83ce-4b05-9c9c-b50c3067e7e1");

        public ICommand NewConversationCommand { get; }

        private ChatPage ChatPage { get; }
        private ChatNewConversationPage ChatNewConversationPage { get; }
        private NavigationPage NavigationChatPage { get; }

        public ConversationPageViewModel(IChatController controller, ChatPage chatPage, ChatNewConversationPage chatNewConversationPage) : base(controller)
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

        public async void SelctedItemHandler(object sender, SelectedItemChangedEventArgs e)
        {
            if (e == null)
                return;

            Debug.WriteLine($"Selected Conversation {e.SelectedItem}");
            Controller.SelectConversation(e.SelectedItem as ChatConversation);
            await RootPage.NavigateToPage(ChatPage);
        }
    }
}
