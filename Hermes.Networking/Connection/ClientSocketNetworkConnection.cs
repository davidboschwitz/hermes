using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Hermes.Networking.Connection
{
    class ClientSocketNetworkConnection : NetworkConnection
    {
        public override string Name => "Socket";

        public ClientSocketNetworkConnection(IPHostEntry ipHostInfo)
        {
            var ipAddress = ipHostInfo.AddressList[0];
            endPoint = new IPEndPoint(ipAddress, 11000);
            socket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        }

        Socket socket;
        EndPoint endPoint;

        private byte[] buffer = new byte[1024];

        public override void Close()
        {
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
        }

        public override void Open()
        {
            socket.Connect(endPoint);
        }

        public override async Task<string> ReceiveString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            while (socket.Available > 0) {
                var count = socket.Receive(buffer);
                stringBuilder.Append(Encoding.ASCII.GetString(buffer, 0, count));
            }
            return stringBuilder.ToString();
        }

        public override async Task SendString(string s)
        {
            byte[] msg = Encoding.ASCII.GetBytes(s);

            // Send the data through the socket.  
            int bytesSent = socket.Send(msg);
        }
    }
}
