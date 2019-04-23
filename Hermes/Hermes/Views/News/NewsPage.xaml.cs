using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Hermes.Capability.News;
using Hermes.ViewModels.News;
using Plugin.Geolocator;
using Xamarin.Forms.Maps;

namespace Hermes.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewsPage : ContentPage
    {
        private double currentLongitude = 0L;
        private double currentLatitude = 0L;

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

        async void OnButtonClicked(object sender, EventArgs e)
        {
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;

            //CancellationTokenSource ctsrc = new CancellationTokenSource(2000);
            var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(2000));

            currentLongitude = position.Longitude;
            currentLatitude = position.Latitude;

            var currentPosition = new Position(currentLatitude, currentLongitude);
        }


        public Boolean CheckIfInRange(Tuple<Double, Double> reportLatLong)
        {

            return false;
        }

    }
}
