using Hermes.Database;
using Hermes.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hermes.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OOBEPage : ContentPage
    {

        public OOBEPage(DatabaseController databaseController, NavigationPage navigationPage)
        {
            InitializeComponent();

            BindingContext = new OOBEViewModel(databaseController, navigationPage);
        }
    }
}