using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Hermes.Capability.News;
using Hermes.ViewModels.News;

namespace Hermes.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewsAdminPage : ContentPage
    {
        public NewsAdminPage(INewsController controller)
        {
            InitializeComponent();

            BindingContext = new NewsAdminViewModel(controller);
        }
    }
}