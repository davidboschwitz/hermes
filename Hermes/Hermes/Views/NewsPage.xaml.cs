using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Hermes.Capability.News;
using Hermes.Database;
using System.Collections.Specialized;
using System.Collections.ObjectModel;
using Hermes.Models;
using Hermes.ViewModels.News;

namespace Hermes.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewsPage : ContentPage
    {
        private const string newLine = "\r\n";

        public NewsPage(INewsController controller)
        {
            InitializeComponent();
            BindingContext = new NewsViewModel(controller);
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (listView.SelectedItem != null)
            {
                NewsReportPage reportPage = new NewsReportPage();
                reportPage.BindingContext = e.SelectedItem as NewsItem;
                listView.SelectedItem = null;
                await Navigation.PushModalAsync(reportPage);
            }
        }

        public Boolean CheckIfInRange(Tuple<Double, Double> reportLatLong)
        {

            return false;
        }

    }
}
