﻿using System;


namespace Hermes.Models
{
    public class FoodAndWaterPin : IPin<FoodAndWaterPin>
    {
        public double Availability;
        
        public FoodAndWaterPin()
        {
            this.Location.Latitude = Double.NaN;
            this.Location.Longitude = Double.NaN;
            this.Resource = "Food and Water";
            this.Availability = 0.0;
        }

        public FoodAndWaterPin(double lat, double lon)
        {
            this.Location.Latitude = lat;
            this.Location.Longitude = lon;
            this.Resource = "Food and Water";
            this.Availability = 0.0;
        }
        
       public String Resource {
            get {
                return this.Resource;
            }
            set {
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

        public void updateAvailablity(double newAvaibility)
        {
            this.Availability = newAvaibility;
        }
    }
}