using Hermes.Capability.Chat;
using Hermes.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hermes.Views.Chat
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChatVerificationCreatorPage : ContentPage
    {
        public ChatVerificationCreatorPage(IChatController controller)
        {
            InitializeComponent();

            BindingContext = new ChatVerificationCreatorViewModel(controller);
        }
    }
}