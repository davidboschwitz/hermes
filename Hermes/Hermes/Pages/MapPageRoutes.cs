using Hermes.Models;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Hermes.Pages
{
    public class MapPageRoutes : ContentPage
    {
        public MapPageRoutes()
        {
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

            customMap.RouteCoordinates.Add(exampleStartPin.Position);
            customMap.RouteCoordinates.Add(exampleEndPin.Position);

            customMap.CustomPins = new List<CustomPin> { exampleStartPin, exampleEndPin};

            customMap.Pins.Add(exampleStartPin);
            customMap.Pins.Add(exampleEndPin);

            customMap.MoveToRegion(MapSpan.FromCenterAndRadius(exampleStartPin.Position, Distance.FromMiles(1.0)));

            Content = customMap;
        }
    }
}
