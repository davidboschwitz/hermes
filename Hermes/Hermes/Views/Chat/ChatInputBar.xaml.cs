using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hermes.Views.Chat
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChatInputBar : ContentView
    {
        public ChatInputBar()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}