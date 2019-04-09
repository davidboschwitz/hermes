using System;
using Hermes.Database;
using SQLite;
using Xamarin.Forms.Maps;

namespace Hermes.Capability.Map
{
    public class PinItem : DatabaseItem
    {
        public string address { get; set; }
        public string information { get; set; }
        public string url { get; set; }
        public string pinType { get; set; }
        public double latitude { get; set; }
        public double longituded { get; set; }

        [Ignore]
        public Position Position { get
            {
                return new Position(latitude, longituded);
            }
        }

        public PinItem()
        {
             
        }

        public PinItem(string address, string information, string url, string pinType, DateTime timeStamp) : base(Guid.NewGuid(), Guid.NewGuid(), new Guid(), DateTime.Now, DateTime.Now, "Pin", "PinItem")
        {
            this.address = address;
            this.information = information;
            this.url = url;
            this.pinType = pinType;
            CreatedTimestamp = timeStamp;
        }

        public override string ToString()
        {
            return $"{base.ToString()} {CreatedTimestamp} {address} {information} {url} {pinType}";
        }
    }
    
}
