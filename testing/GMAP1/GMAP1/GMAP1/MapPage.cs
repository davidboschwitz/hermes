using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace GMAP1
{
    public class MapPage : ContentPage
    {
        public MapPage()
        {
            //InitializeComponent();

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
                Label = "Test Custom Pin",
                Address = "Test",
                Id = "Ames",
                Url = "http://iastate.edu"
            };

            customMap.CustomPins = new List<CustomPin> { pin };
            customMap.Pins.Add(pin);
            customMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(42.025250, -93.650870), Distance.FromMiles(1.0)));

            /*
            var stack = new StackLayout { Spacing = 0 };
            stack.Children.Add(customMap);
            Content = stack;
            */

            Content = customMap;
            
        }
    }
}
