using System.Collections.Generic;
using Hermes.Networking;
using Hermes.Database;
using System.Collections.ObjectModel;
using System;
using System.ComponentModel;

namespace Hermes.Capability.Map
{
    //[HermesNotifyNamespace(Capability.Namespace)]
    //[HermesSyncTable(typeof(PinItem))]
    public class MapsController : ICapabilityController
    {
        private DatabaseController DatabaseController;

        public event Action<Type, DatabaseItem> SendMessage;
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<PinItem> Pins { get; }

        public MapsController(DatabaseController databaseController)
        {
            DatabaseController = databaseController;

            //Initialize();

            Pins = new ObservableCollection<PinItem>();

            DatabaseController.CreateTable<PinItem>();
            foreach (var PinItem in DatabaseController.Table<PinItem>())
            {
                Pins.Add(PinItem);
            }
        }

        public void savePins(List<PinItem> pins)
        {
            DatabaseController.InsertAll(pins);
        }

        public void OnNotification(string messageNamespace, string messageName, Guid messageID)
        {
            throw new NotImplementedException();
        }
    }
}
