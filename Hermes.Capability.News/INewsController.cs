using System.Collections.ObjectModel;

namespace Hermes.Capability.News
{
    public interface INewsController
    {
        ObservableCollection<NewsItem> Feed { get; }
    }
}
