using Hermes.Capability.Permissions;
using Xamarin.Forms;

namespace Hermes.Menu
{
    public class HermesMenuItem
    {
        public HermesMenuItem(string title, ContentPage contentPage) : this(title, contentPage, PermissionsController.Level.USER)
        {
        }
        public HermesMenuItem(string title, ContentPage contentPage, PermissionsController.Level accessLevel)
        {
            Title = title;
            ContentPage = contentPage;
            NavigationPage = new NavigationPage(contentPage);
            AccessLevel = accessLevel;
        }

        public string Title { get; set; }

        public ContentPage ContentPage { get; }

        public NavigationPage NavigationPage { get; }

        public PermissionsController.Level AccessLevel { get; }
    }
}
