using System.Collections.Generic;
using Hermes.Networking;
using Hermes.Database;
using System.Collections.ObjectModel;

namespace Hermes.Capability.Map
{
    //[HermesNotifyNamespace("Map")]
    //[HermesSyncTable(typeof(PinItem))]
    public class MapsController 
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
