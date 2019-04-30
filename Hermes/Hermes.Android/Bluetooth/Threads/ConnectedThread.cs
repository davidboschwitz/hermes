//using Android.App;
//using Android.Bluetooth;
//using Android.Util;
//using Android.Widget;
//using System.IO;

//namespace Hermes.Droid.Bluetooth.Threads
//{
//    /// <summary>
//    /// This thread runs during a connection with a remote device.
//    /// It handles all incoming and outgoing transmissions.
//    /// </summary>
//    public class ConnectedThread : Java.Lang.Thread
//    {
//        private const string TAG = "Hermes.Bluetooth.Threads.ConnectedThread";
//        BluetoothSocket Socket;
//        BluetoothAdapter Adapter;
//        BluetoothDevice Device;
//        Stream inStream;
//        Stream outStream;

//        public ConnectedThread(BluetoothSocket socket, BluetoothDevice device, BluetoothAdapter adapter, string socketType)
//        {
//            Log.Debug(TAG, $"create ConnectedThread: {socketType}");
//            Socket = socket;
//            Device = device;
//            Adapter = adapter;
//            Stream tmpIn = null;
//            Stream tmpOut = null;

//            // Get the BluetoothSocket input and output streams
//            try
//            {
//                tmpIn = socket.InputStream;
//                tmpOut = socket.OutputStream;
//            }
//            catch (Java.IO.IOException e)
//            {
//                Log.Error(TAG, "temp sockets not created", e);
//            }

//            inStream = tmpIn;
//            outStream = tmpOut;
//            Toast.MakeText(Application.Context, $"Connected to {Device.Name}", ToastLength.Long).Show();
//            //service.state = BluetoothChatService.STATE_CONNECTED;
//        }

//        public BluetoothSocketConnection ToNetworkConnection()
//        {
//            return null;//new BluetoothSocketConnection(Socket, inStream, outStream);
//        }

//        public override void Run()
//        {
//            Log.Info(TAG, "BEGIN mConnectedThread");
//            byte[] buffer = new byte[1024];
//            int bytes;

//            // Keep listening to the InputStream while connected
//            //while (service.GetState() == BluetoothChatService.STATE_CONNECTED)
//            //{
//            //    try
//            //    {
//            //        // Read from the InputStream
//            //        bytes = inStream.Read(buffer, 0, buffer.Length);

//            //        // Send the obtained bytes to the UI Activity
//            //        service.handler
//            //               .ObtainMessage(Constants.MESSAGE_READ, bytes, -1, buffer)
//            //               .SendToTarget();
//            //    }
//            //    catch (Java.IO.IOException e)
//            //    {
//            //        Log.Error(TAG, "disconnected", e);
//            //        service.ConnectionLost();
//            //        break;
//            //    }
//            //}
//        }

//        /// <summary>
//        /// Write to the connected OutStream.
//        /// </summary>
//        /// <param name='buffer'>
//        /// The bytes to write
//        /// </param>
//        public void Write(byte[] buffer)
//        {
//            try
//            {
//                outStream.Write(buffer, 0, buffer.Length);

//                // Share the sent message back to the UI Activity
//                //service.handler
//                //       .ObtainMessage(Constants.MESSAGE_WRITE, -1, -1, buffer)
//                //       .SendToTarget();
//            }
//            catch (Java.IO.IOException e)
//            {
//                Log.Error(TAG, "Exception during write", e);
//            }
//        }

//        public void Cancel()
//        {
//            try
//            {
//                Socket.Close();
//            }
//            catch (Java.IO.IOException e)
//            {
//                Log.Error(TAG, "close() of connect socket failed", e);
//            }
//        }
//    }
//}
