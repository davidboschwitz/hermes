using Hermes.Capability.Chat;

namespace Hermes.ViewModels.Chat
{
    public class ChatBaseViewModel : BaseViewModel
    {
        private IChatController controller;
        public IChatController Controller
        {
            get { return controller; }
            set { SetProperty(ref controller, value); }
        }

        public ChatBaseViewModel(IChatController controller)
        {
            Controller = controller;
        }
    }
}
