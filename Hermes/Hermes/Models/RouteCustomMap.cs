using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Maps;

namespace Hermes.Models
{
    public class RouteCustomMap : Map
    {
        public List<CustomPin> CustomPins { get; set; }
        public List<Position> RouteCoordinates { get; set; }

        public RouteCustomMap()
        {
            RouteCoordinates = new List<Position>();
        }

        public event EventHandler<MapTapEventArgs> Tapped;

        public void OnTap(Position coordinate)
        {
            OnTap(new MapTapEventArgs { Position = coordinate });
        }

        protected virtual void OnTap(MapTapEventArgs e)
        {
            Tapped?.Invoke(this, e);
        }
    }
}
