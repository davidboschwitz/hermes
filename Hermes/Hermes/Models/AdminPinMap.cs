using System;
using System.Collections.Generic;
using Xamarin.Forms.Maps;


namespace Hermes.Models
{
    public class AdminPinMap : Map
    {
        public List<CustomPin> CustomPins { get; set; }

        public event EventHandler<MapTapEventArgs> Tapped;

        protected virtual void OnTap(MapTapEventArgs e)
        {
            Tapped?.Invoke(this, e);
        }        
    }
}