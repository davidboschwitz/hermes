using Autofac;
using Hermes.Capability.Chat;
using Hermes.Capability.Chat.Model;
using Hermes.Capability.Map;
using Hermes.Capability.News;
using Hermes.Capability.Permissions;
using Hermes.Database;
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

            // Go to http://aka.ms/dotnet-get-started-console to continue learning how to build a console app! 
            program = new Program();
        }

        IContainer Container;
        NetworkController NetworkController;
        DatabaseController DatabaseController;
        SocketListener SocketListener;

        /* Capbaility Controllers */
        ChatController ChatController;
        NewsController NewsController;
        MapsController MapsController;
        PermissionsController PermissionsController;

        Program()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new ServerModule());

            Container = builder.Build();

            NetworkController = Container.Resolve<NetworkController>();
            SocketListener = Container.Resolve<SocketListener>();
            DatabaseController = Container.Resolve<DatabaseController>();

            ChatController = Container.Resolve<ChatController>();
            NewsController = Container.Resolve<NewsController>();
            MapsController = Container.Resolve<MapsController>();
            PermissionsController = Container.Resolve<PermissionsController>();

            Console.WriteLine("Press any key to start listener");
            Console.ReadKey();
            new Thread(SocketListener.Run).Start();
            CommandLoop();
        }

        void CommandLoop()
        {
            var running = true;
            while (running)
            {
                Console.Write("HERMES>");
                string line = Console.ReadLine();
                var args = line.Split(' ');
                string arg(int i) { if (i > 0 && i < args.Length) { return args[i]; } return null; };
                switch (args[0])
                {
                    case "stop":
                        SocketListener.Stop();
                        running = false;
                        break;
                    case "chat":
                        if ("contact".Equals(arg(1)))
                        {
                            if ("list".Equals(arg(2)))
                            {
                                Console.WriteLine("ID\t\t\t\t\tName");
                                Console.WriteLine("--\t\t\t\t\t----");
                                foreach (var contact in ChatController.Contacts)
                                {
                                    Console.WriteLine($"{contact.ID}\t{contact.Name}");
                                }
                            }
                            else if ("update".Equals(arg(2)))
                            {
                                if (Guid.TryParse(arg(3), out var id))
                                {
                                    //TODO: update contact
                                    Console.WriteLine("chat contact update is not yet implemented");
                                }
                                else
                                {
                                    Console.WriteLine("Please input a valid guid");
                                }
                            }
                            else if ("add".Equals(arg(2)))
                            {
                                Guid id;
                                string name;
                                if (Guid.TryParse(arg(3), out id) && !string.IsNullOrWhiteSpace(arg(4)))
                                {
                                    name = arg(4);
                                    ChatController.CreateContact(new ChatContact(id, name));
                                    Console.WriteLine($"Added contact {name} ({id})");
                                }
                                else if ("*".Equals(arg(3)) && !string.IsNullOrWhiteSpace(arg(4)))
                                {
                                    id = Guid.NewGuid();
                                    name = arg(4);
                                    ChatController.CreateContact(new ChatContact(id, name));
                                    Console.WriteLine($"Added contact {name} ({id})");
                                }
                                else
                                {
                                    Console.WriteLine("usage: chat contact add [guid|*(new guid)] [name]");
                                }
                            }
                            else
                            {
                                Console.WriteLine("usage: chat contact [list|update|add]");
                            }
                        }
                        else if ("message".Equals(arg(2)))
                        {

                        }
                        else
                        {
                            Console.WriteLine("usage: chat [contact|message]");
                        }
                        break;
                    case "permission":
                        if ("grant".Equals(arg(1)))
                        {
                            if ("list".Equals(arg(2)))
                            {
                                Console.WriteLine("ID\t\t\t\t\tLevel");
                                Console.WriteLine("--\t\t\t\t\t-----");
                                foreach (var grant in PermissionsController.Grants)
                                {
                                    Console.WriteLine($"{grant.MessageID}\t{grant.Grant}");
                                }
                            }
                            else if ("add".Equals(arg(2)) || "update".Equals(arg(2)))
                            {
                                Guid id;
                                PermissionsController.Level level;
                                if (Guid.TryParse(arg(3), out id) && Enum.TryParse(arg(4), out level))
                                {
                                    PermissionsController.Update(id, level);
                                }
                                else
                                {
                                    Console.WriteLine($"usage permission {arg(2)} [guid] [USER|ADMIN|SUPER]");
                                }
                            }
                            else
                            {
                                Console.WriteLine("usage: permission grant [list|add|update]");
                            }
                        }
                        else
                        {
                            Console.WriteLine("usage: permission [grant [list|add|update]]");
                        }
                        break;
                    case "news":
                        if ("list".Equals(arg(1)))
                        {
                            Console.WriteLine("ID\t\t\t\t\tTitle\t\t\t\tBody");
                            Console.WriteLine("--\t\t\t\t\t-----\t\t\t\t----");
                            foreach (var n in NewsController.Feed)
                            {
                                Console.WriteLine($"{n.MessageID}\t{n.Title}\n{n.Body}\n");
                            }
                        }
                        else if ("add".Equals(arg(1)))
                        {
                            Console.Write("Enter Title: ");
                            var title = Console.ReadLine();
                            Console.Write("Enter Body (ESC to finish): ");
                            var body = "";
                            ConsoleKeyInfo c;
                            do
                            {
                                c = Console.ReadKey();
                                if (c.Key == ConsoleKey.Escape)
                                {
                                    break;
                                }
                                else if (c.Key == ConsoleKey.Backspace)
                                {
                                    body.Substring(0, body.Length - 1);
                                }
                                else if(c.Key == ConsoleKey.Enter)
                                {
                                    body += '\n';
                                    Console.WriteLine();
                                }
                                else
                                {
                                    body += c.KeyChar;
                                }
                            } while (true);
                            var n = new NewsItem(title, body, DateTime.Now);
                            NewsController.InsertReport(n.Title, n.Body);
                            Console.WriteLine("Created new NewsItem");
                            Console.WriteLine($"{n.MessageID}\t{n.Title}\n{n.Body}\n");
                        }
                        else if ("update".Equals(arg(1)))
                        {
                            Console.WriteLine("Not yet implemneted");
                        }
                        else
                        {
                            Console.WriteLine("usage: news [list|add|update]");
                        }
                        break;
                    case "help":
                        Console.WriteLine("Commands: chat news permission stop");
                        break;
                    case "":
                        break;
                    default:
                        Console.WriteLine($"Command '{args[0]}' unknown");
                        break;
                }
            }
        }
    }
}
