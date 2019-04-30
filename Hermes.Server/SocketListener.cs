using Hermes.Networking;
using Hermes.Networking.Connection;

using System;
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
            var port = 6969;
            var server = TcpListener.Create(port);
            server.Start();

            Console.WriteLine($"Listener started on port {port}");

            while (run)
            {
                // here pending requests are in a queue.
                if (server.Pending())
                {
                    new Thread(() =>
                    {
                        var socket = server.AcceptSocket();
                        Console.WriteLine($"New connection from {socket.RemoteEndPoint}!");
                        NetworkController.Sync(new SocketNetworkConnection(socket), new UpdateAllSequence());
                    }).Start();
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
