using Hermes.Capability.News;

namespace Hermes.ViewModels.News
{
    public class NewsViewModel : BaseViewModel
    {
        private INewsController controller;
        public INewsController Controller
        {
            get { return controller; }
            set { SetProperty(ref controller, value); }
        }

        public NewsViewModel(INewsController controller)
        {
            Controller = controller;
        }

    }
}
