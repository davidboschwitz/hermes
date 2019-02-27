using Hermes.Models;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Hermes.Pages
{
    class SupplyPinPage : ContentPage
    {
        public SupplyPinPage()
        {
            SupplyPinMap customMap = new SupplyPinMap()
            {
                HeightRequest = 100,
                WidthRequest = 960,
                VerticalOptions = LayoutOptions.FillAndExpand,
                MapType = MapType.Street,
            };

            var supplyPin = new CustomPin
            {
                Type = PinType.Place,
                Position = new Position(42.02290, -93.63912),
                Address = " - need to possibly implement - ",
                Id = "supplies",
                Label = "supplies",
                Url = "http://www.redcross.org"
            };

            customMap.CustomPins = new List<CustomPin> { supplyPin };
            customMap.Pins.Add(supplyPin);
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
