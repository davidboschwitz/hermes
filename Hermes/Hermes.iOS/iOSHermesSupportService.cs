using Hermes.Services;
using UIKit;

namespace Hermes.iOS
{
    public class iOSHermesSupportService : HermesSupportService
    {
        public override string GetUniqueIdentifier()
        {
            return UIDevice.CurrentDevice.IdentifierForVendor.AsString();
        }
    }
}