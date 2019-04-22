using Hermes.Models;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

using Plugin.Geolocator;
using System;
using System.Threading;

namespace Hermes.Pages
{
    public class MapPageRoutes : ContentPage
    {
        public MapPageRoutes()
        {
            var currentLatitude = 0.0;
            var currentLongitude = 0.0;
            ControlTemplate = (ControlTemplate)Application.Current.Resources["MainPageTemplate"];

            RouteCustomMap customMap = new RouteCustomMap()
            {
                HeightRequest = 100,
                WidthRequest = 960,
                VerticalOptions = LayoutOptions.FillAndExpand,
                MapType = MapType.Street,
            };

            var exampleStartPin = new CustomPin
            {
                Type = PinType.Place,
                Position = new Position(42.02332, -93.66791),
                Address = " - need to possibly implement - ",
                Id = "supplies",
                Label = "supplies",
                Url = "https://www.redcross.org/store"
            };

            var exampleEndPin = new CustomPin
            {
                Type = PinType.Place,
                Position = new Position(42.020454, -93.60969),
                Address = " - need to possibly implement - ",
                Id = "supplies",
                Label = "supplies",
                Url = "https://www.redcross.org/store"
            };

            var getLocation = new Button
            {
                Text = "Get Location"
            };

            getLocation.Clicked += new EventHandler(OnButtonClicked);

            NavigationPage.SetHasNavigationBar(this, false);

            async void OnButtonClicked(object sender, EventArgs e)
            {
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 50;

                //CancellationTokenSource ctsrc = new CancellationTokenSource(2000);
                var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(2000));

                currentLongitude = position.Longitude;
                currentLatitude = position.Latitude;

                var currentPosition = new Position(currentLatitude, currentLongitude);
                customMap.MoveToRegion(MapSpan.FromCenterAndRadius(currentPosition, Distance.FromMiles(0.05)));
            }

            customMap.RouteCoordinates.Add(exampleStartPin.Position);
            customMap.RouteCoordinates.Add(exampleEndPin.Position);

            customMap.CustomPins = new List<CustomPin> { exampleStartPin, exampleEndPin};

            customMap.Pins.Add(exampleStartPin);
            customMap.Pins.Add(exampleEndPin);

            customMap.MoveToRegion(MapSpan.FromCenterAndRadius(exampleStartPin.Position, Distance.FromMiles(1.0)));

            Content = new StackLayout
            {
                Spacing = 0,
                Children = {
                    customMap,
                    getLocation
                }
            };
        }
    }
}
