using Hermes.Models;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Hermes
{
    public class MapPage : ContentPage
    {
        public MapPage()
        {
            CustomMap customMap = new CustomMap()
            {
                MapType = MapType.Street,
                WidthRequest = App.ScreenWidth,
                HeightRequest = App.ScreenHeight
            };

            var examplePinSupplies = new CustomPin
            {
                Type = PinType.Place,
                Position = new Position(42.02525, -93.65087),
                Address = " - need to possibly implement - ",
                Id = "supplies",
                Label = "supplies",
                Url = "https://www.redcross.org/store"
            };

            var examplePinMedical = new CustomPin
            {
                Type = PinType.Place,
                Position = new Position(42.02290, -93.63912),
                Address = " - need to possibly implement - ",
                Id = "medical",
                Label = "medical",
                Url = "http://www.redcross.org"
            };

            var examplePinShelter = new CustomPin
            {
                Type = PinType.Place,
                Position = new Position(42.02045, -93.60968),
                Address = " - need to possibly implement - ",
                Id = "shelter",
                Label = "shelter",
                Url = "http://www.redcross.org"
            };

            customMap.CustomPins = new List<CustomPin> { examplePinSupplies, examplePinMedical, examplePinShelter };
            customMap.Pins.Add(examplePinSupplies);
            customMap.Pins.Add(examplePinMedical);
            customMap.Pins.Add(examplePinShelter);
            customMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(42.025250, -93.650870), Distance.FromMiles(1.0)));

            Content = customMap;
        }
    }
}
