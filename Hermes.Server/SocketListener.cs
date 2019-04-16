using Hermes.Networking;
using Hermes.Networking.Connection;

using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Hermes.Server
{
    public class SocketListener
    {
        private NetworkController NetworkController;
        private bool run = true;

        public SocketListener(NetworkController networkController)
        {
            NetworkController = networkController;
        }

        public void Run()
        {
            IPHostEntry host = Dns.GetHostEntry("localhost");
            IPAddress ip = IPAddress.Parse("192.168.0.58");//(host.AddressList.First().GetAddressBytes());
            var port = 6969;
            var server = new TcpListener(ip, port);
            server.Start();

            Console.WriteLine($"Listener started on port {ip}:{port}");

            while (run)
            {
                // here pending requests are in a queue.
                if (server.Pending())
                {
                    Console.WriteLine("New connection!");
                    new Thread(() => NetworkController.Sync(new SocketNetworkConnection(server.AcceptSocket()), new UpdateAllSequence())).Start();
                }
            }
            server.Stop();
            Console.WriteLine("Stopped");
        }

        public void Stop()
        {
            Console.WriteLine("Stopping listener...");
            run = false;
        }
    }
}
