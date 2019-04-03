using System;
using System.Collections.Generic;
using Xamarin.Forms.Maps;

namespace Hermes.Models
{
    public class MapTapEventArgs : EventArgs
    {
        public Position Position { get; set; }
    }
}
