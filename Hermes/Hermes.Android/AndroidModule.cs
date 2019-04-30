using Android.Bluetooth;
using Autofac;
using Hermes.Droid.Bluetooth;
using Hermes.Networking;
using Hermes.Services;

namespace Hermes.Droid
{
    public class AndroidModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new AndroidHermesSupportService())
                   .As<IHermesSupportService>()
                   .SingleInstance();

            var toast = new AndroidToastService();
            builder.Register(c => toast)
                   .As<IHermesToastService>()
                   .SingleInstance();
            
            if (BluetoothAdapter.DefaultAdapter == null)
            {
                System.Diagnostics.Debug.WriteLine("Bluetooth is not available!!");
                toast.LongAlert("Bluetooth is not available");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Initializing Bluetooth");

                builder.Register(c => new AndroidBluetoothService(c.Resolve<NetworkController>()))
                       .As<IHermesBluetoothService>()
                       .SingleInstance();
            }
        }
    }
}
