using System;
using System.Collections.Generic;
using Xamarin.Forms.Maps;

namespace Hermes.Models
{
    public class CustomMap : Map
    {
        public List<CustomPin> CustomPins { get; set; }

        public event EventHandler<MapTapEventArgs> Tapped;

        public void OnTap(Position coordinate)
        {
            OnTap(new MapTapEventArgs { position = coordinate });
        }

        protected virtual void OnTap(MapTapEventArgs e)
        {
            var handler = Tapped;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }

    public class MapTapEventArgs : EventArgs
    {
        public Position position { get; set; }
    }
}