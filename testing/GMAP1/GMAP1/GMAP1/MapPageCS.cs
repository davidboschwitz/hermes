using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace GMAP1
{
    class MapPageCS : ContentPage
    {
        public MapPageCS()
        {
            var customMap = new CustomMap
            {
                MapType = MapType.Street,
                WidthRequest = App.ScreenWidth,
                HeightRequest = App.ScreenHeight
            };

            Content = customMap;
        }
    }
}
