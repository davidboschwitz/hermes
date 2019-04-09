using System;
using System.Collections.Generic;
using System.Text;
using Hermes.Database;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Hermes.Capability.Map
{
    public class MapsController : IMapController
    {
        private DatabaseController DatabaseController;

        public ObservableCollection<PinItem> Pins { get; }

        public MapsController(DatabaseController databaseController)
        {
            DatabaseController = databaseController;

            //Initialize();

            Pins = new ObservableCollection<PinItem>();

            DatabaseController.CreateTable<PinItem>();
            foreach(var PinItem in DatabaseController.Table<PinItem>())
            {
                Pins.Add(PinItem);
            }
        }

        public void savePins(List<PinItem> pins)
        {
            DatabaseController.InsertAll(pins);   
        }
    }
}
