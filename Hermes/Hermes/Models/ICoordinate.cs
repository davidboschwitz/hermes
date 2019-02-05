using System;
using System.Collections.Generic;
using System.Text;

namespace Hermes.Models
{
    public interface ICoordinate
    {
        double Longitude { get; set; }
        double Latitude { get; set; }
    }
}
