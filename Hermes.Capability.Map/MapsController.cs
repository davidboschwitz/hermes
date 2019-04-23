using System.Collections.Generic;
using Hermes.Networking;
using Hermes.Database;
using System.Collections.ObjectModel;
using System;
using System.ComponentModel;
using System.Diagnostics;

namespace Hermes.Capability.Map
{
    [HermesNotifyNamespace(Capability.Namespace)]
    [HermesSyncTable(typeof(PinItem))]
    public class MapsController : ICapabilityController
    {
        private DatabaseController DatabaseController;

        public event Action<Type, DatabaseItem> SendMessage;
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<PinItem> Pins { get; }

        public MapsController(DatabaseController databaseController)
        {
            DatabaseController = databaseController;

            Pins = new ObservableCollection<PinItem>();

            DatabaseController.CreateTable<PinItem>();
            foreach (var PinItem in DatabaseController.Table<PinItem>())
            {
                Pins.Add(PinItem);
            }
        }

        public void savePin(PinItem pin)
        {
            Debug.WriteLine(pin.Information);
            DatabaseController.Insert(pin);
        }

        //private void Initialize()
        //{
        //    DatabaseController.CreateTable<PinItem>();
        //    /*Dummy Data*/
        //    PinItem p1 = new PinItem();
            
        //    //Check for existing items
        //    DatabaseController.Insert(p1);
        //}

        public void OnNotification(string messageNamespace, string messageName, Guid messageID)
        {
            throw new NotImplementedException();
        }
    }
}
