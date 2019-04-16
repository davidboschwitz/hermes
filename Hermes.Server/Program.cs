using Autofac;
using Hermes.Networking;
using System;
using System.Threading;

namespace Hermes.Server
{
    class Program
    {
        private static Program program;
        static void Main(string[] args)
        {
            // The code provided will print ‘Hello World’ to the console.
            // Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.
            Console.WriteLine("Hermes.Server Starting up...");
            Console.ReadKey();

            // Go to http://aka.ms/dotnet-get-started-console to continue learning how to build a console app! 
            program = new Program(); 
        }

        IContainer Container;
        NetworkController NetworkController;
        SocketListener SocketListener;

        Program()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new ServerModule());

            Container = builder.Build();

            NetworkController = Container.Resolve<NetworkController>();
            SocketListener = Container.Resolve<SocketListener>();
            Console.WriteLine("Press any key to start listener");
            Console.ReadKey();
            new Thread(SocketListener.Run).Start();
        }
    }
}
