using System.Collections.Generic;
using Hermes.Networking;
using Hermes.Database;
using System.Collections.ObjectModel;
using System;
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
            Debug.WriteLine(pin.Information);
            DatabaseController.Insert(pin);
            Pins.Add(pin);
            OnPropertyChanged("Pins");
        }

        public void OnNotification(string messageNamespace, string messageName, Guid messageID)
        {
            throw new NotImplementedException();
        }

        #region INotifyPropertyChanged
        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName]string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

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
