using Android.App;
using Android.Widget;
using Hermes.Services;

namespace Hermes.Droid
{
    public class AndroidToastService : IHermesToastService
    {
        public void LongAlert(string msg)
        {
            Toast.MakeText(Application.Context, msg, ToastLength.Long).Show();
        }

        public void ShortAlert(string msg)
        {
            Toast.MakeText(Application.Context, msg, ToastLength.Short).Show();
        }
    }
}