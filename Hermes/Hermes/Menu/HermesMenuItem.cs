using Xamarin.Forms;

namespace Hermes.Menu
{
    public class HermesMenuItem
    {
        public HermesMenuItem(string title, ContentPage contentPage)
        {
            Title = title;
            ContentPage = contentPage;
            NavigationPage = new NavigationPage(contentPage);
        }

        public string Title { get; set; }

        public ContentPage ContentPage { get; }

        public NavigationPage NavigationPage { get; }
    }
}
