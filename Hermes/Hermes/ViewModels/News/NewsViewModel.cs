using Hermes.Capability.News;

namespace Hermes.ViewModels.News
{
    public class NewsViewModel : BaseViewModel
    {
        private NewsController controller;
        public NewsController Controller
        {
            get { return controller; }
            set { SetProperty(ref controller, value); }
        }

        public NewsViewModel(NewsController controller)
        {
            Controller = controller;
        }

    }
}
