using Hermes.Networking;
using Hermes.Networking.Connection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hermes.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NetSyncPageWow : ContentPage
    {
        NetworkController Controller;
        public NetSyncPageWow(NetworkController controller)
        {
            InitializeComponent();
            Controller = controller;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Debug.WriteLine("Button_Clicked");
            var socket = new Socket(AddressFamily.InterNetwork,
        SocketType.Stream,
        ProtocolType.Tcp);
            var host = "192.168.0.58";
            var port = 6969;
            socket.Connect(host, port);
            Debug.WriteLine($"{socket.Connected}, {socket.RemoteEndPoint}");
            var nc = new SocketNetworkConnection(socket);
            var seq = new UpdateAllSequence();
            Task.Run(() => Controller.Sync(nc, seq));
        }
    }
}