using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace GMAP1
{
    public class App : Application // superclass new in 1.3
    {
        public App()
        {

            var tabs = new TabbedPage();

            // demonstrates the map control with zooming and map-types
            tabs.Children.Add(new MapPage { Title = "Map/Zoom"});

            // demonstrates the map control with zooming and map-types
            tabs.Children.Add(new PinPage { Title = "Pins", Icon = "Icons/pin.png" });

            MainPage = tabs;
        }
    }
}
