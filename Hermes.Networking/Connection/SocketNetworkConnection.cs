using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Hermes.Networking.Connection
{
    public class SocketNetworkConnection : NetworkConnection
    {
        public override string Name => "Socket";

        public SocketNetworkConnection(Socket socket)
        {
            Socket = socket;
        }

        Socket Socket { get; }

        public override void Close()
        {
            Socket.Shutdown(SocketShutdown.Both);
            Socket.Close();
        }


        public override void Open()
        {
            //Socket should already be open.
        }

        const string EOF_STREAM = "<EOF>";
        private string nextMSG = "";
        public override async Task<string> ReceiveString()
        {
            if (nextMSG.Length > 5)
            {
                Console.WriteLine($"nextmsg:{nextMSG}");
                Debug.WriteLine($"nextmsg:{nextMSG}");

                var rtnx = nextMSG.Substring(0, nextMSG.IndexOf(EOF_STREAM));
                nextMSG = rtnx.Substring(nextMSG.IndexOf(EOF_STREAM));
                Console.WriteLine($"RecievedStringcached:{rtnx}");
                Debug.WriteLine($"RecievedStringcached:{rtnx}");
                return rtnx;
            }
            // Data buffer 
            byte[] bytes = new Byte[1024];
            string data = null;

            while (true)
            {
                int numByte = Socket.Receive(bytes);
                data += Encoding.ASCII.GetString(bytes, 0, numByte);

                if (data.Contains(EOF_STREAM))
                    break;
            }
            
            Console.WriteLine($"data:{data}");
            Debug.WriteLine($"data:{data}");
            var rtn = data.Substring(0, data.IndexOf(EOF_STREAM));
            nextMSG = data.Substring(data.IndexOf(EOF_STREAM) + EOF_STREAM.Length);
            Console.WriteLine($"RecievedString:{rtn}");
            Debug.WriteLine($"RecievedString:{rtn}");
            return rtn;
        }

        public override async Task SendString(string s)
        {
            byte[] message = Encoding.ASCII.GetBytes(s + EOF_STREAM);
            Socket.Send(message);
            Console.WriteLine($"SendString:{s}");
            Debug.WriteLine($"SendString:{s}");
        }
    }
}
