using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Maps;

namespace Hermes.Models
{
    public class JustViewCustomMap : Map
    {
        public List<CustomPin> CustomPins { get; set; }
    }
}
