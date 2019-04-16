using Hermes.Networking;
using System.Collections.ObjectModel;

namespace Hermes.Capability.News
{
    [HermesNotifyNamespace(Capability.Namespace)]
    [HermesSyncTable(typeof(NewsItem))]
    public interface INewsController : ICapabilityController
    {
        ObservableCollection<NewsItem> Feed { get; }

        void InsertReport(string title, string body);
    }
}
