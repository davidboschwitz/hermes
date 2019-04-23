using Hermes.Models;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Hermes.Capability.Map;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Hermes.Pages
{
    public class MapPage : ContentPage
    {
        MapsController Controller;
        
        public MapPage(MapsController controller)
        {
            Controller = controller;

            ObservableCollection<PinItem> dbPins = controller.Pins;

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

            var outputPins = new List<CustomPin>();

            foreach (var p in dbPins)
            {
                Debug.WriteLine(p.Address);
                var tempPin = new CustomPin
                {
                    Type = PinType.Place,
                    Position = p.Position,
                    Address = p.Address,
                    Id = p.PinType,
                    Label = p.PinType,
                    Url = p.Url
                };

                outputPins.Add(tempPin);
                
            }

            outputPins.Add(examplePinSupplies);

            customMap.CustomPins = outputPins;
            
            foreach(var pin in outputPins)
            {
                Debug.WriteLine(pin.Address);
                Debug.WriteLine(pin.Label);
                customMap.Pins.Add(pin);
            }

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
