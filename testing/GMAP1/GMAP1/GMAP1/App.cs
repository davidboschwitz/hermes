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
            tabs.Children.Add(new MapPage { Title = "Map/Zoom", Icon = "glyphish_74_location.png" });

            // demonstrates the map control with zooming and map-types
            tabs.Children.Add(new PinPage { Title = "Pins", Icon = "glyphish_07_map_marker.png" });

            MainPage = tabs;
        }
    }
}
