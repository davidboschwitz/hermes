using System;
using System.Collections.Generic;
using Xamarin.Forms.Maps;

namespace Hermes.Models
{
    public class CustomMap : Map
    {
        public List<CustomPin> CustomPins { get; set; }
    }
}