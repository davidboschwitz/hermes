using System;
using System.Diagnostics;

namespace Hermes.Services
{
    public abstract class HermesSupportService : IHermesSupportService
    {
        public abstract string GetUniqueIdentifier();

        public Guid HermesIdentifier()
        {
            if (hermesIdentifier == null)
                hermesIdentifier = new Guid(GetUniqueIdentifier());
            Debug.WriteLine($"UID: {GetUniqueIdentifier()}");
            Debug.WriteLine($"HID: {hermesIdentifier}");
            return hermesIdentifier;
        }
        private Guid hermesIdentifier;
    }
}
