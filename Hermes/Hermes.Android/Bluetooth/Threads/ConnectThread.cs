using Android.App;
using Android.Bluetooth;
using Android.Util;
using Android.Widget;
using Java.Util;
using System.IO;
using System.Threading.Tasks;

namespace Hermes.Droid.Bluetooth.Threads
{
    /// <summary>
    /// This thread runs while attempting to make an outgoing connection
    /// with a device. It runs straight through; the connection either
    /// succeeds or fails.
    /// </summary>
    public class ConnectThread
    {
        public static UUID MY_UUID_SECURE = UUID.FromString("fa87c0d0-afac-11de-8a39-0800200c9a66");
        public static UUID MY_UUID_INSECURE = UUID.FromString("8ce255c0-200a-11e0-ac64-0800200c9a66");

        private const string TAG = "Hermes.Bluetooth.Threads.ConnectThread";
        BluetoothSocket Socket;
        BluetoothDevice Device;
        BluetoothAdapter Adapter;
        AndroidBluetoothService Service;
        string socketType;

        Stream inStream;
        Stream outStream;

        public ConnectThread(BluetoothDevice device, BluetoothAdapter adapter, AndroidBluetoothService service, bool secure)
        {
            Device = device;
            Adapter = adapter;
            Service = service;
            BluetoothSocket tmp = null;
            socketType = secure ? "Secure" : "Insecure";

            try
            {
                if (secure)
                {
                    tmp = device.CreateRfcommSocketToServiceRecord(MY_UUID_SECURE);
                }
                else
                {
                    tmp = device.CreateInsecureRfcommSocketToServiceRecord(MY_UUID_INSECURE);
                }

            }
            catch (Java.IO.IOException e)
            {
                Log.Error(TAG, "create() failed", e);
            }
            Socket = tmp;
            //service.state = BluetoothChatService.STATE_CONNECTING;
        }

        public async Task<BluetoothSocketConnection> Run()
        {
            // Always cancel discovery because it will slow down connection
            Adapter.CancelDiscovery();
            Service.CurrentState = AndroidBluetoothService.State.CONNECTING;

            // Make a connection to the BluetoothSocket
            try
            {
                // This is a blocking call and will only return on a
                // successful connection or an exception
                Socket.Connect();
            }
            catch (Java.IO.IOException e)
            {
                Log.Error(TAG, "Failure connecting to socket", e);
                // Close the socket
                try
                {
                    Socket.Close();
                }
                catch (Java.IO.IOException e2)
                {
                    Log.Error(TAG, $"unable to close() {socketType} socket during connection failure.", e2);
                }

                // Start the service over to restart listening mode
                //service.ConnectionFailed();
                Toast.MakeText(Application.Context, $"Could not connect to {Device.Name}", ToastLength.Long).Show();
                Service.CurrentState = AndroidBluetoothService.State.NONE;
                return null;
            }
            Stream tmpIn = null;
            Stream tmpOut = null;

            // Get the BluetoothSocket input and output streams
            try
            {
                tmpIn = Socket.InputStream;
                tmpOut = Socket.OutputStream;
            }
            catch (Java.IO.IOException e)
            {
                Log.Error(TAG, "temp sockets not created", e);
            }

            inStream = tmpIn;
            outStream = tmpOut;

            Toast.MakeText(Application.Context, $"Connected to {Device.Name}", ToastLength.Long).Show();
            Service.CurrentState = AndroidBluetoothService.State.CONNECTED;

            return new BluetoothSocketConnection(Service, Socket, inStream, outStream);
        }

        public void Cancel()
        {
            try
            {
                Socket.Close();
            }
            catch (Java.IO.IOException e)
            {
                Log.Error(TAG, "close() of connect socket failed", e);
            }
            Service.CurrentState = AndroidBluetoothService.State.NONE;
        }
    }
}
