using Hermes.Models;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Hermes.Pages
{
    class ShelterPinPage : ContentPage
    {
        public ShelterPinPage()
        {
            ShelterPinMap customMap = new ShelterPinMap()
            {
                HeightRequest = 100,
                WidthRequest = 960,
                VerticalOptions = LayoutOptions.FillAndExpand,
                MapType = MapType.Street,
            };

            var shelterPin = new CustomPin
            {
                Type = PinType.Place,
                Position = new Position(42.02290, -93.63912),
                Address = " - need to possibly implement - ",
                Id = "shelter",
                Label = "shelter",
                Url = "http://www.redcross.org"
            };

            customMap.CustomPins = new List<CustomPin> { shelterPin };
            customMap.Pins.Add(shelterPin);
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
