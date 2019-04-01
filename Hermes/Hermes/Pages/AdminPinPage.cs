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
                //TODO:
                //Save pin to DB


            }

            async void back_clickedAsync(object sender, EventArgs e)
            {
                await Navigation.PopModalAsync();
            }

            customMap.CustomPins = new List<CustomPin> { newPin };
            customMap.Pins.Add(newPin);
            customMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(42.025250, -93.650870), Distance.FromMiles(1.0)));

            Content = new StackLayout
            {
                Spacing = 0,
                Children = {
                    customMap
                    //confirm,
                    //back
                }
            };
        }
    }
}
