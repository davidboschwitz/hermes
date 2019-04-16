using Hermes.Models;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Hermes.Capability.Map;
using System.Collections.ObjectModel;

namespace Hermes.Pages
{
    public class MapPage : ContentPage
    {
        public MapPage()
        {
            MapsController controller;
            ObservableCollection<PinItem> receivingPins = controller.Pins;

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

            customMap.CustomPins = new List<CustomPin> { examplePinSupplies };
            customMap.Pins.Add(examplePinSupplies);
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
