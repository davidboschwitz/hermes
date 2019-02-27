using Hermes.Models;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Hermes.Pages
{
    public class MapPage : ContentPage
    {
        public MapPage()
        {
            OriginalCustomMap customMap = new OriginalCustomMap()
            {
                HeightRequest = 100,
                WidthRequest = 960,
                VerticalOptions = LayoutOptions.FillAndExpand,
                MapType = MapType.Street,
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

            /*
            var addPin = new ToggleButton { Text = "Add pin" };

            var buttons = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children = {
                    addPin
                }
            };

            Content = new StackLayout
            {
                Spacing = 0,
                Children = {
                    customMap,
                    buttons
                }
            };
            */
        }
    }
}
