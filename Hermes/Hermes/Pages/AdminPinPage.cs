using Hermes.Capability.Map;
using Hermes.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Hermes.Pages
{
    class AdminPinPage : ContentPage
    {  
        public AdminPinPage(CustomPin pin)
        {
            MapsController controller;

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
                List<PinItem> list = new List<PinItem>();

                PinItem dbPin = new PinItem
                {
                    Address = newPin.Address,
                    Information = "",
                    Url = newPin.Url,
                    PinType = newPin.Label,
                    Latitude = newPin.Position.Latitude,
                    Longitude = newPin.Position.Longitude
                };

                list.Add(dbPin);

                controller.savePins(list);
            }

            async void back_clickedAsync(object sender, EventArgs e)
            {
                await Navigation.PopModalAsync();
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
