using Hermes.Networking;
using Hermes.Networking.Connection;
using Hermes.Services;
using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hermes.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NetSyncPageWow : ContentPage
    {
        NetworkController Controller;
        IHermesBluetoothService Bluetooth;

        public NetSyncPageWow(NetworkController controller, IHermesBluetoothService bluetooth)
        {
            InitializeComponent();
            Controller = controller;
            Bluetooth = bluetooth;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Debug.WriteLine("Button_Clicked");
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            var host = Controller.CloudURL;
            var port = 6969;
            socket.Connect(host, port);
            Debug.WriteLine($"{socket.Connected}, {socket.RemoteEndPoint}");
            var nc = new SocketNetworkConnection(socket);
            var seq = new UpdateAllSequence();
            Task.Run(() => Controller.Sync(nc, seq));
        }

        private void Button_Bluetooth(object sender, EventArgs e)
        {
            //Bluetooth.PairWithBlueToothDevice(false);
            Bluetooth?.Start();
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if(e.SelectedItem is HermesBluetoothDevice device)
            {
                Debug.WriteLine($"NetSync: Attempting to connect to {device.Name} ({device.Address})");
                Bluetooth?.Connect(device.Address, false);
            }
            ListOfDevices.SelectedItem = null;
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            Bluetooth.Start();
            Bluetooth.RefreshPairedDevices();
            ListOfDevices.ItemsSource = Bluetooth.PairedDevices;
        }
    }
}