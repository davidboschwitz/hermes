using Hermes.Services;
using System;
using System.Diagnostics;
using Windows.System.Profile;

namespace Hermes.UWP
{
    public class UWPHermesSupportService : HermesSupportService
    {
        private string uniqueIdentifier = string.Empty;
        public override string GetUniqueIdentifier()
        {
            if (uniqueIdentifier != string.Empty)
            {
                return uniqueIdentifier;
            }

            var token = HardwareIdentification.GetPackageSpecificToken(null);
            var hardwareId = token.Id;
            var dataReader = Windows.Storage.Streams.DataReader.FromBuffer(hardwareId);

            byte[] bytes = new byte[hardwareId.Length];
            Debug.WriteLine(hardwareId.Length);
            dataReader.ReadBytes(bytes);

            return uniqueIdentifier = BitConverter.ToString(bytes);
        }
    }
}
