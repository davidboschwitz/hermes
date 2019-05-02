using Hermes.Database;
using Hermes.Networking;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Hermes.Capability.Map
{
    [HermesNotifyNamespace(Capability.Namespace)]
    [HermesSyncTable(typeof(PinItem))]
    public class MapsController : ICapabilityController
    {
        private DatabaseController DatabaseController;

        public event Action<Type, DatabaseItem> SendMessage;

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

        public void SavePin(PinItem pin)
        {
            DatabaseController.Insert(pin);
            Pins.Add(pin);
            OnPropertyChanged("Pins");
        }

        //public void DeletePin(PinItem pin)
        //{
        //    DatabaseController.Delete(pin);
        //    Pins.Remove(pin);
        //    OnPropertyChanged("Pins");
        //}

        public void OnNotification(string messageNamespace, string messageName, Guid messageID)
        {
            Debug.WriteLine($"[MapsController]: OnNotification({messageNamespace}, {messageName}, {messageID})");
            if (Capability.Namespace.Equals(messageNamespace))
            {
                if (Capability.MessageNames.PinItem.Equals(messageName))
                {
                    var msg = DatabaseController.Table<PinItem>().Where(m => m.MessageID.Equals(messageID)).First();
                    Pins.Add(msg);
                    OnPropertyChanged("Pins");
                }
            }
        }

        #region INotifyPropertyChanged
        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName]string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
            {
                return false;
            }

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
