using Android.App;
using Android.Bluetooth;
using Android.Widget;
using Hermes.Networking;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Hermes.Droid.Bluetooth
{
    public class BluetoothSocketConnection : NetworkConnection
    {
        private const string EOF_STREAM_DELIMITER = "<\"EOF\">";

        public override string Name => "BluetoothSocketConnection";
        public override Encoding Encoding => Encoding.UTF8;

        AndroidBluetoothService Service { get; }
        BluetoothSocket Socket { get; }
        Stream InStream { get; }
        Stream OutStream { get; }

        public BluetoothSocketConnection(AndroidBluetoothService service, BluetoothSocket socket, Stream inStream, Stream outStream)
        {
            Service = service;
            Socket = socket;
            InStream = inStream;
            OutStream = outStream;
        }

        public override async Task SendString(string s)
        {
            byte[] buffer = Encoding.GetBytes(s + EOF_STREAM_DELIMITER);
            try
            {
                OutStream.Write(buffer, 0, buffer.Length);
            }
            catch (Java.IO.IOException e)
            {
                Toast.MakeText(Application.Context, "Error in sending content via bluetooth.", ToastLength.Long).Show();
                Debug.WriteLine("BluetoothSocketConnection: Error in ReceiveString()");
                Debug.WriteLine(e);
                Debug.WriteLine(e.Message);
            }
        }

        private string nextMSG = "";
        public override async Task<string> ReceiveString()
        {
            if (nextMSG.Length > EOF_STREAM_DELIMITER.Length)
            {
                Debug.WriteLine($"nextmsg:{nextMSG}");

                var rtnx = nextMSG.Substring(0, nextMSG.IndexOf(EOF_STREAM_DELIMITER));
                nextMSG = rtnx.Substring(nextMSG.IndexOf(EOF_STREAM_DELIMITER));
                Debug.WriteLine($"RecievedStringcached:{rtnx}");
                return rtnx;
            }

            int bytes;
            byte[] buffer = new byte[2048];
            string data = null;

            try
            {
                while (true)
                {
                    // Read from the InputStream
                    bytes = InStream.Read(buffer, 0, buffer.Length);
                    data += Encoding.GetString(buffer, 0, bytes);

                    // Send the obtained bytes to the UI Activity
                    if (data.Contains(EOF_STREAM_DELIMITER))
                    {
                        break;
                    }
                }
            }
            catch (Java.IO.IOException e)
            {
                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                    Toast.MakeText(Application.Context, "Error in receiving content via bluetooth.", ToastLength.Long).Show());
                Debug.WriteLine("BluetoothSocketConnection: Error in ReceiveString()");
                Debug.WriteLine(e);
                Debug.WriteLine(e.Message);
            }

            Debug.WriteLine($"data:{data}");
            var rtn = data.Substring(0, data.IndexOf(EOF_STREAM_DELIMITER));
            nextMSG = data.Substring(data.IndexOf(EOF_STREAM_DELIMITER) + EOF_STREAM_DELIMITER.Length);
            Debug.WriteLine($"RecievedString:{rtn}");
            return rtn;
        }

        public override void Open()
        {

        }

        public override void Close()
        {
            try
            {
                Socket.Close();
            }
            catch (Java.IO.IOException e)
            {
                Debug.WriteLine("BluetoothSocketConnection: Error in Close()");
                Debug.WriteLine(e);
                Debug.WriteLine(e.Message);
            }
            Service.CurrentState = AndroidBluetoothService.State.NONE;
        }
    }
}
