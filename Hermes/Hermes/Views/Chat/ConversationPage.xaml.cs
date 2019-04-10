using Hermes.Capability.Chat;
using Hermes.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hermes.Views.Chat
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConversationPage : ContentPage
    {
        public ConversationPage(IChatController controller, ChatPage chatPage)
        {
            InitializeComponent();

            var vm = new ConversationPageViewModel(controller, chatPage);
            BindingContext = vm;
            
            ConversationListView.ItemSelected += vm.SelctedItemHandler;

            //Convert.FromBase64String();
        }
    }
}
