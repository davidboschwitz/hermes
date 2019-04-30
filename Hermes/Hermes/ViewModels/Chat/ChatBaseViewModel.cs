using Hermes.Capability.Chat;

namespace Hermes.ViewModels.Chat
{
    public class ChatBaseViewModel : BaseViewModel
    {
        private ChatController controller;
        public ChatController Controller
        {
            get { return controller; }
            set { SetProperty(ref controller, value); }
        }

        public ChatBaseViewModel(ChatController controller)
        {
            Controller = controller;
        }
    }
}
