using Hermes.Capability.Chat;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hermes.Views.Chat
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChatBubbleVerificationReceived : ViewCell
    {
        ChatController Controller { get; }
        public ChatBubbleVerificationReceived(ChatController controller)
        {
            InitializeComponent();

            Controller = controller;
        }

        private void Accept_Button_Clicked(object sender, System.EventArgs e)
        {
            Controller.AcceptVerification();
        }
    }
}
