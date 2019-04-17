using Hermes.Capability.Chat;
using Hermes.ViewModels;

using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hermes.Views.Chat
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChatPage : ContentPage
    {
        public ChatPage(IChatController controller)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            var vm = new ChatPageViewModel(controller);
            BindingContext = vm;

            vm.ScrollToLast += () =>
            {
                MessagesListView.ScrollTo(controller.CurrentConversation?.Messages.Last(), ScrollToPosition.End, true);
            };
        }
    }
}
