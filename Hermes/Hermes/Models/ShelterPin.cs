using System;

namespace Hermes.Models
{
    public class ShelterPin : IPin<FoodPin>
    {

        public ShelterPin(double lat, double lon)
        {
            this.Location.Latitude = lat;
            this.Location.Longitude = lon;
            this.Resource = Resource;
        }

        public String Resource
        {
            get
            {
                return this.Resource;
            }
            set
            {
                this.Resource = value;
            }
        }
        public ICoordinate Location
        {
            get
            {
                return this.Location;
            }
            set
            {
                this.Location = value;
            }
        }
    }
}
