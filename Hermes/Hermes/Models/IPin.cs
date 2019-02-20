using System;
using System.Collections.Generic;
using System.Text;

namespace Hermes.Models
{
    public interface IPin<T>
    {
        String Resource { get; set; }
        ICoordinate Location { get; set; }
        
    }
}
