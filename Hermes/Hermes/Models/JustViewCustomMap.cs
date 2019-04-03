using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Maps;

namespace Hermes.Models
{
    public class JustViewCustomMap : Map
    {
        public List<CustomPin> CustomPins { get; set; }

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
