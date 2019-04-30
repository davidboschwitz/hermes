using System.Collections.ObjectModel;

namespace Hermes.Services
{
    public interface IHermesBluetoothService
    {
        void Start();
        void RefreshPairedDevices();
        ObservableCollection<HermesBluetoothDevice> PairedDevices { get; }
        void Connect(string address, bool secure);
        void EnsureDiscoverable();
    }
    public class HermesBluetoothDevice
    {
        public string Name { get; }
        public string Address { get; }

        public HermesBluetoothDevice(string name, string address)
        {
            Name = name;
            Address = address;
        }
    }
}
