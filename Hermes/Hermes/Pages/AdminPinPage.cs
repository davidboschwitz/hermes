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
            
            customMap.CustomPins = new List<CustomPin> { newPin };
            customMap.Pins.Add(newPin);
            customMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(42.025250, -93.650870), Distance.FromMiles(1.0)));

            Content = new StackLayout
            {
                Spacing = 0,
                Children = {
                    customMap,
                }
            };
        }
    }
}
