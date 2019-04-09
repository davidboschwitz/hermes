using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Hermes.Capability.Map
{
    interface IMapController
    {
       ObservableCollection<PinItem> Pins { get; }
    }
}
