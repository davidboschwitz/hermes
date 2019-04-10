using System;
using System.Collections.Generic;
using System.Text;

namespace Hermes.Capability.Map
{
    interface IMapController
    {
        //b
       // List<Layer> AllLayers();
       // List<Layer> ActiveLayers();
        void RemoveLayer();
        void AddLayer();
        void Update();
        void Clear();

    }
}
