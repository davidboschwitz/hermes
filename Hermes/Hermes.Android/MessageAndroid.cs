using System.Runtime.Remoting.Messaging;

using Android.App;
using Android.Widget;
using Hermes.Droid;
using Hermes.Models;

[assembly: Xamarin.Forms.Dependency(typeof(MessageAndroid))]
namespace Hermes.Droid
{
    public class MessageAndroid : Models.IMessage
    {
        public void LongAlert(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Long).Show();
        }

        public void ShortAlert(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Short).Show();
        }
    }
}
