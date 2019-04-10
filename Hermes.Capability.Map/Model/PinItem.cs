using System;
using Hermes.Database;
using SQLite;
using Xamarin.Forms.Maps;

namespace Hermes.Capability.Map
{
    public class PinItem : DatabaseItem
    {
        public string Address { get; set; }
        public string Information { get; set; }
        public string Url { get; set; }
        public string PinType { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        [Ignore]
        public Position Position => new Position(Latitude, Longitude);

        public PinItem()
        {
             
        }

        public PinItem(string address, string information, string url, string pinType, DateTime timeStamp) : base(Guid.NewGuid(), Guid.NewGuid(), new Guid(), DateTime.Now, DateTime.Now, Capability.Namespace, Capability.MessageNames.PinItem)
        {
            Address = address;
            Information = information;
            Url = url;
            PinType = pinType;
            CreatedTimestamp = timeStamp;
        }

        public override string ToString()
        {
            return $"{base.ToString()} {CreatedTimestamp} {Address} {Information} {Url} {PinType}";
        }
    }
    
}
