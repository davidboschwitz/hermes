using Xamarin.Forms;

namespace Hermes.Menu
{
    public enum HermesMenuItemType
    {
        Chat,
        About,
        News,
        Map
    }
    public class HermesMenuItem
    {
        public HermesMenuItem(HermesMenuItemType type, string title, ContentPage contentPage)
        {
            Type = type;
            Title = title;
            ContentPage = contentPage;
            NavigationPage = new NavigationPage(contentPage);
        }

        public HermesMenuItemType Type { get; set; }

        public string Title { get; set; }

        public ContentPage ContentPage { get; }

        public NavigationPage NavigationPage { get; }
    }
}
