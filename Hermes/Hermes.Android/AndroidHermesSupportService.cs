using Hermes.Services;

namespace Hermes.Droid
{
    public class AndroidHermesSupportService : HermesSupportService
    {
        public override string GetUniqueIdentifier()
        {
            return Android.Provider.Settings.Secure.GetString(Xamarin.Forms.Forms.Context.ContentResolver, Android.Provider.Settings.Secure.AndroidId);
        }
    }
}