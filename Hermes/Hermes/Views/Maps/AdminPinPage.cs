using Hermes.Capability.Map;
using Hermes.Models;
using Hermes.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Hermes.Pages
{
    class AdminPinPage : ContentPage
    {
        protected MainPage RootPage => Application.Current.MainPage as MainPage;
        MapsController Controller;

        public AdminPinPage(CustomPin pin, MapsController controller)
        {
            Controller = controller;

            AdminPinMap customMap = new AdminPinMap()
            {
                HeightRequest = 100,
                WidthRequest = 960,
                VerticalOptions = LayoutOptions.FillAndExpand,
                MapType = MapType.Street,
            };

            var newPin = pin;

            Button confirm = new Button
            {
                Text = "Confirm"
            };

            Button back = new Button
            {
                Text = "back"
            };

            confirm.Clicked += confirm_clicked;
            back.Clicked += back_clickedAsync;

            void confirm_clicked(object sender, EventArgs e)
            {
                DateTime timestamp = DateTime.Now;

                var dbPin = new PinItem(newPin.Address,newPin.Information,newPin.Url,newPin.Label,timestamp);          

                Controller.SavePin(dbPin);

                RootPage.SetNavigationRoot(new NavigationPage(new PinInfoPage(Controller)));
            }

            async void back_clickedAsync(object sender, EventArgs e)
            {
                RootPage.NavigatePop();
            }

            var centerLat = newPin.Position.Latitude;
            var centerLon = newPin.Position.Longitude;

            customMap.CustomPins = new List<CustomPin> { newPin };
            customMap.Pins.Add(newPin);
            customMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(centerLat, centerLon), Distance.FromMiles(1.0)));

            Content = new StackLayout
            {
                Spacing = 0,
                Children = {                   
                    confirm,
                    back,
                    customMap
                }
            };
        }
    }
}
