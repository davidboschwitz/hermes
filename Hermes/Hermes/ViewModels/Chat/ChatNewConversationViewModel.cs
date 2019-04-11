using Hermes.Capability.Chat;
using Hermes.Capability.Chat.Model;
using Hermes.Views.Chat;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace Hermes.ViewModels.Chat
{
    public class ChatNewConversationViewModel : ChatBaseViewModel
    {
        private ChatPage ChatPage;

        public ICommand NewContactCommand { get; }

        public ChatNewConversationViewModel(IChatController controller, ChatPage chatPage) : base(controller)
        {
            ChatPage = chatPage;

            NewContactCommand = new Command(NewContactFunctionAsync);
        }

        private async void NewContactFunctionAsync(object e)
        {
            await RootPage.NavigateToPage(new AddContactPage(Controller));
        }

        public async void SelectContactHandler(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is ChatContact contact)
            {
                Controller.SelectConversation(contact);
                await RootPage.NavigateToPageReplaceLast(ChatPage);
            }
        }

        private void AddContactFunction()
        {

        }

    }
}
