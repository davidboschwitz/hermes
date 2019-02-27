using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms.Maps;

namespace Hermes.Models
{
    public class MapTapEventArgs : EventArgs
    {
        public Position Position { get; set; }
    }
}