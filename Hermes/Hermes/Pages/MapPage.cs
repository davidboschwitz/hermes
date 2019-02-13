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

            var pin = new CustomPin
            {
                Type = PinType.Place,
                Position = new Position(42.025250, -93.650870),
                Label = "Iowa State Food Shelter",
                Address = "2229 Lincoln Way Ames, IA",
                Id = "FoodPin1",
                Url = "http://iastate.edu"
            };

            customMap.CustomPins = new List<CustomPin> { pin };
            customMap.Pins.Add(pin);
            customMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(42.025250, -93.650870), Distance.FromMiles(1.0)));

            Content = customMap;

        }
    }

    
}
