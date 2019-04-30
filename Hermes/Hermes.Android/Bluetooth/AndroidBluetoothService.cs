using Android.App;
using Android.Bluetooth;
using Android.Content;
using Android.Widget;
using Hermes.Droid.Bluetooth.Threads;
using Hermes.Networking;
using Hermes.Services;
using Java.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Hermes.Droid.Bluetooth
{
    public class AndroidBluetoothService : IHermesBluetoothService, INotifyPropertyChanged
    {
        public enum State
        {
            NONE = 0,       // we're doing nothing
            LISTEN = 1,     // now listening for incoming connections
            CONNECTING = 2, // now initiating an outgoing connection
            CONNECTED = 3,  // now connected to a remote device
        }
        BluetoothAdapter Adapter = BluetoothAdapter.DefaultAdapter;

        private ConnectThread ConnectThread { get; set; }
        private AcceptThread SecureAcceptThread { get; set; }
        private AcceptThread InsecureAcceptThread { get; set; }

        private NetworkController NetworkController { get; }

        private State currentState;
        public State CurrentState
        {
            get { return currentState; }
            set { SetProperty(ref currentState, value); }
        }

        public const string NAME_SECURE = "BluetoothChatSecure";
        public const string NAME_INSECURE = "BluetoothChatInsecure";

        public static UUID MY_UUID_SECURE = UUID.FromString("fa87c0d0-afac-11de-8a39-0800200c9a66");
        public static UUID MY_UUID_INSECURE = UUID.FromString("8ce255c0-200a-11e0-ac64-0800200c9a66");

        private static NetworkSequence DefaultSequence = new UpdateAllSequence();

        public AndroidBluetoothService(NetworkController networkController)
        {
            NetworkController = networkController;
        }

        public void Start()
        {
            Stop();

            if (SecureAcceptThread == null)
            {
                SecureAcceptThread = new AcceptThread(Adapter, this, true);
                SecureAcceptThread.Start();
            }
            if (InsecureAcceptThread == null)
            {
                InsecureAcceptThread = new AcceptThread(Adapter, this, false);
                InsecureAcceptThread.Start();
            }
            Toast.MakeText(Application.Context, "Started listening for incoming bluetooth connections", ToastLength.Long).Show();
        }

        public async void Connect(string address, bool secure)
        {
            if (CurrentState == State.CONNECTED || CurrentState == State.CONNECTING)
            {
                Debug.WriteLine($"Did not connect to ({address}), connection already in progress");
                Toast.MakeText(Application.Context, "Please wait until the current connection is complete before starting a new one.", ToastLength.Short).Show();
                return;
            }
            var device = Adapter.GetRemoteDevice(address);

            Stop();

            CurrentState = State.CONNECTING;

            // Start the thread to connect with the given device
            ConnectThread = new ConnectThread(device, Adapter, this, secure);
            var connection = await ConnectThread.Run();
            Sync(connection);
        }

        void Stop()
        {
            if (SecureAcceptThread != null)
            {
                SecureAcceptThread.Cancel();
                SecureAcceptThread = null;
            }
            if (InsecureAcceptThread != null)
            {
                InsecureAcceptThread.Cancel();
                InsecureAcceptThread = null;
            }
            if (ConnectThread != null)
            {
                ConnectThread.Cancel();
                ConnectThread = null;
            }
            CurrentState = State.NONE;
        }

        internal void Sync(BluetoothSocketConnection connection)
        {
            if (connection == null)
            {
                Debug.WriteLine("Could not connect to bluetooth device");
                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                    Toast.MakeText(Application.Context, "Unable to establish bluetooth connection to device", ToastLength.Long).Show());
                return;
            }
            Debug.WriteLine($"Syncing Bluetooth Connection");
            Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                Toast.MakeText(Application.Context, "Syncing via bluetooth...", ToastLength.Long).Show());
            NetworkController.Sync(connection, DefaultSequence);
        }

        public ObservableCollection<HermesBluetoothDevice> PairedDevices
        {
            get
            {
                ObservableCollection<HermesBluetoothDevice> devices = new ObservableCollection<HermesBluetoothDevice>();
                foreach (var device in Adapter.BondedDevices)
                {
                    devices.Add(new HermesBluetoothDevice(device.Name, device.Address));
                }
                foreach (var d in devices)
                {
                    Debug.WriteLine($"BluetoothBonded({d.Name}, {d.Address})");
                }

                return devices;
            }
        }

        public void RefreshPairedDevices()
        {
            OnPropertyChanged("PairedDevices");
        }

        public void EnsureDiscoverable()
        {
            if (Adapter.ScanMode != ScanMode.ConnectableDiscoverable)
            {
                var discoverableIntent = new Intent(BluetoothAdapter.ActionRequestDiscoverable);
                discoverableIntent.PutExtra(BluetoothAdapter.ExtraDiscoverableDuration, 300);
                Application.Context.StartActivity(discoverableIntent);
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
