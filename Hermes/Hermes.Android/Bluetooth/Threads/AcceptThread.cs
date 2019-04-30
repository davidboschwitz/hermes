using Android.App;
using Android.Bluetooth;
using Android.Util;
using Android.Widget;
using System.Diagnostics;
using System.IO;

namespace Hermes.Droid.Bluetooth.Threads
{
    /// <summary>
    /// This thread runs while listening for incoming connections. It behaves
    /// like a server-side client. It runs until a connection is accepted
    /// (or until cancelled).
    /// </summary>
    public class AcceptThread : Java.Lang.Thread
    {
        // The local server socket
        BluetoothServerSocket serverSocket;
        string socketType;
        BluetoothAdapter Adapter;
        AndroidBluetoothService Service;

        public AcceptThread(BluetoothAdapter adapter, AndroidBluetoothService service, bool secure)
        {
            Adapter = adapter;
            Service = service;
            BluetoothServerSocket tmp = null;
            socketType = secure ? "Secure" : "Insecure";
            Name = $"AcceptThread_{socketType}";

            try
            {
                if (secure)
                {
                    tmp = Adapter.ListenUsingRfcommWithServiceRecord(AndroidBluetoothService.NAME_SECURE, AndroidBluetoothService.MY_UUID_SECURE);
                }
                else
                {
                    tmp = Adapter.ListenUsingInsecureRfcommWithServiceRecord(AndroidBluetoothService.NAME_INSECURE, AndroidBluetoothService.MY_UUID_INSECURE);
                }

            }
            catch (Java.IO.IOException e)
            {
                Debug.WriteLine($"{Name}: listen() failed");
                Debug.WriteLine(e.Message);
                //Log.Error(TAG, "listen() failed", e);
            }
            serverSocket = tmp;
            Service.CurrentState = AndroidBluetoothService.State.LISTEN;
        }

        public override void Run()
        {
            BluetoothSocket socket = null;

            while (Service.CurrentState != AndroidBluetoothService.State.CONNECTED)
            {
                try
                {
                    socket = serverSocket.Accept();
                }
                catch (Java.IO.IOException e)
                {
                    Debug.WriteLine($"{Name}: accept() failed");
                    Debug.WriteLine(e.Message);
                    //Log.Error(TAG, "accept() failed", e);
                    break;
                }

                if (socket != null)
                {
                    lock (this)
                    {
                        switch (Service.CurrentState)
                        {
                            case AndroidBluetoothService.State.LISTEN:
                            case AndroidBluetoothService.State.CONNECTING:
                                // Situation normal. Start the connected thread.
                                Service.CurrentState = AndroidBluetoothService.State.CONNECTING;
                                Stream inStream = null;
                                Stream outStream = null;

                                // Get the BluetoothSocket input and output streams
                                try
                                {
                                    inStream = socket.InputStream;
                                    outStream = socket.OutputStream;
                                }
                                catch (Java.IO.IOException e)
                                {
                                    Log.Error(Name, "temp sockets not created", e);
                                    //Toast.MakeText(Application.Context, $"Failed to create connection from {socket.RemoteDevice.Name} via bluetooth", ToastLength.Long).Show();
                                    Service.CurrentState = AndroidBluetoothService.State.NONE;
                                    return;
                                }

                                //Toast.MakeText(Application.Context, $"Connected to {socket.RemoteDevice.Name} via bluetooth", ToastLength.Long).Show();
                                Service.CurrentState = AndroidBluetoothService.State.CONNECTED;

                                var connection = new BluetoothSocketConnection(Service, socket, inStream, outStream);
                                Service.Sync(connection);
                                break;
                            case AndroidBluetoothService.State.NONE:
                            case AndroidBluetoothService.State.CONNECTED:
                                try
                                {
                                    socket.Close();
                                }
                                catch (Java.IO.IOException e)
                                {
                                    Debug.WriteLine("could not close unwanted socket");
                                    Debug.WriteLine(e.Message);
                                    //Log.Error(TAG, "Could not close unwanted socket", e);
                                }
                                break;
                        }
                    }
                }
            }
        }

        public void Cancel()
        {
            try
            {
                serverSocket.Close();
            }
            catch (Java.IO.IOException e)
            {
                Debug.WriteLine("close() failed");
                Debug.WriteLine(e.Message);
                //Log.Error(TAG, "close() of server failed", e);
            }
        }
    }
}