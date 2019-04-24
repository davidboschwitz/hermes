using System;
using System.Collections.Generic;
using System.Net;

using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Widget;
using Hermes.Droid;
using Hermes.Models;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.Android;
using Newtonsoft.Json;
using Plugin.Geolocator;

[assembly: ExportRenderer(typeof(OriginalCustomMap), typeof(OriginalCustomRenderer))]

namespace Hermes.Droid
{
    class OriginalCustomRenderer : MapRenderer, GoogleMap.IInfoWindowAdapter, IOnMapReadyCallback
    {

        List<CustomPin> customPins;

        public OriginalCustomRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(Xamarin.Forms.Platform.Android.ElementChangedEventArgs<Map> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                NativeMap.InfoWindowClick -= OnInfoWindowClick;
            }

            if (e.NewElement != null)
            {
                var formsMap = (OriginalCustomMap)e.NewElement;
                customPins = formsMap.CustomPins;
                Control.GetMapAsync(this);
            }
        }

        protected override void OnMapReady(GoogleMap map)
        {
            base.OnMapReady(map);

            NativeMap.InfoWindowClick += OnInfoWindowClick;
            NativeMap.SetInfoWindowAdapter(this);

            //Below is the logic for tapping on the screen to add pins directly at that location
            //This has been replaced with a different way to add pins, so this is being removed 
            //Also applies to the GoogleMap_MapClick() method below
            //
            //if (map != null)
            //{
            //    map.MapClick += GoogleMap_MapClick;
            //}
        }

        //private void GoogleMap_MapClick(object sender, GoogleMap.MapClickEventArgs e)
        //{
        //    ((OriginalCustomMap)Element).OnTap(new Position(e.Point.Latitude, e.Point.Longitude));
        //    var addingPin = new CustomPin
        //    {
        //        Type = PinType.Place,
        //        Position = new Position(e.Point.Latitude, e.Point.Longitude),
        //        Address = " - need to possibly implement - ",
        //        Id = "shelter",
        //        Label = "shelter",
        //        Url = "http://www.redcross.org"
        //    };

        //    Map.Pins.Add(addingPin);
        //    customPins.Add(addingPin);
        //}

        protected override MarkerOptions CreateMarker(Pin pin)
        {
            var marker = new MarkerOptions();
            marker.SetPosition(new LatLng(pin.Position.Latitude, pin.Position.Longitude));
            marker.SetTitle(pin.Label);
            marker.SetSnippet(pin.Address);
            return marker;
        }

        public Android.Views.View GetInfoContents(Marker marker)
        {
            if (Android.App.Application.Context.GetSystemService(Context.LayoutInflaterService) is Android.Views.LayoutInflater inflater)
            {
                Android.Views.View view;

                var customPin = GetCustomPin(marker);
                if (customPin == null)
                {
                    throw new Exception("Custom pin not found");
                }

                if (customPin.Label.ToString().Equals("supplies"))
                {
                    view = inflater.Inflate(Resource.Layout.MapInfoWindow_Supplies, null);
                }
                else if (customPin.Label.ToString().Equals("medical"))
                {
                    view = inflater.Inflate(Resource.Layout.MapInfoWindow_Medical, null);
                }
                else if (customPin.Label.ToString().Equals("shelter"))
                {
                    view = inflater.Inflate(Resource.Layout.MapInfoWindow_Shelter, null);
                }
                else
                {
                    //DependencyService.Get<IMessage>().LongAlert("Resorting to default view");
                    view = inflater.Inflate(Resource.Layout.MapInfoWindow_Supplies, null);
                }

                var infoTitle = view.FindViewById<TextView>(Resource.Id.InfoWindowTitle);
                var infoSubtitle = view.FindViewById<TextView>(Resource.Id.InfoWindowSubtitle);

                if (infoTitle != null)
                {
                    infoTitle.Text = marker.Title;
                }
                if (infoSubtitle != null)
                {
                    infoSubtitle.Text = marker.Snippet;
                }

                return view;
            }

            return null;
        }

        public Android.Views.View GetInfoWindow(Marker marker)
        {
            return null;
        }

        void OnInfoWindowClick(object sender, GoogleMap.InfoWindowClickEventArgs e)
        {
            //All of this is to have the info window clickable open the URL that is displayed
            //This has been replaced with logic for a route button that will take precident if
            //      there is no way to add them both to the info window, which there might not be
            /*
            var customPin = GetCustomPin(e.Marker);
            if (customPin == null)
            {
                throw new Exception("Custom pin not found");
            }

            if (!string.IsNullOrWhiteSpace(customPin.Url))
            {
                var url = Android.Net.Uri.Parse(customPin.Url);
                var intent = new Intent(Intent.ActionView, url);
                intent.AddFlags(ActivityFlags.NewTask);
                Android.App.Application.Context.StartActivity(intent);
            }
            */

            var customPin = GetCustomPin(e.Marker);
            if(customPin == null)
            {
                DependencyService.Get<IMessage>().LongAlert("Custom pin not found!");
                throw new Exception("Custom pin not found!");
            }

            OnButtonClicked(sender, e, customPin.Position);
        }

        CustomPin GetCustomPin(Marker annotation)
        {
            var position = new Position(annotation.Position.Latitude, annotation.Position.Longitude);
            foreach (var pin in customPins)
            {
                if (pin.Position == position)
                {
                    return pin;
                }
            }
            return null;
        }

        List<Position> FnDecodePolylinePoints(string encodedPoints)
        {
            if (encodedPoints == null || encodedPoints == "") return null;
            List<Position> poly = new List<Position>();
            char[] polylinechars = encodedPoints.ToCharArray();
            int index = 0;
            int currentLat = 0;
            int currentLng = 0;
            int next5bits;
            int sum;
            int shifter;
            try
            {
                while (index < polylinechars.Length)
                {
                    // calculate next latitude
                    sum = 0;
                    shifter = 0;
                    do
                    {
                        next5bits = (int)polylinechars[index++] - 63;
                        sum |= (next5bits & 31) << shifter;
                        shifter += 5;
                    } while (next5bits >= 32 && index < polylinechars.Length);
                    if (index >= polylinechars.Length)
                        break;
                    currentLat += (sum & 1) == 1 ? ~(sum >> 1) : (sum >> 1);
                    //calculate next longitude
                    sum = 0;
                    shifter = 0;
                    do
                    {
                        next5bits = (int)polylinechars[index++] - 63;
                        sum |= (next5bits & 31) << shifter;
                        shifter += 5;
                    } while (next5bits >= 32 && index < polylinechars.Length);
                    if (index >= polylinechars.Length && next5bits >= 32)
                        break;
                    currentLng += (sum & 1) == 1 ? ~(sum >> 1) : (sum >> 1);
                    Position p = new Position(Convert.ToDouble(currentLat) / 100000.0,
                        Convert.ToDouble(currentLng) / 100000.0);
                    poly.Add(p);
                }
            }
            catch (Exception e)
            {
                if (e.Source != null)
                    Console.WriteLine("Exception source: {0}", e.Source);
                throw;
            }
            return poly;
        }

        public void DrawRoute(GoogleMap g, Position currentPosition, Position destination)
        {
            var lat1 = currentPosition.Latitude;
            var long1 = currentPosition.Longitude;
            var lat2 = destination.Latitude;
            var long2 = destination.Longitude; 

            //below is the concatinated url to check outputs
            //url = https://maps.googleapis.com/maps/api/directions/json?origin=42.02332,-93.66791&destination=42.020454,-93.60969&key=AIzaSyAJ80rolI5NRBXgrvwylHrcAQBGhii_1dI
            var url = "https://maps.googleapis.com/maps/api/directions/json?" +
                "origin=" + lat1 + "," + long1 +
                "&destination=" + lat2 + "," + long2 +
                "&key=" + "AIzaSyAJ80rolI5NRBXgrvwylHrcAQBGhii_1dI";

            var json = "";
            using (WebClient wc = new WebClient())
            {
                json = wc.DownloadString(url);
            }

            RootObject myr = JsonConvert.DeserializeObject<RootObject>(json);

            //DependencyService.Get<IMessage>().LongAlert("checking how many GWP in root:" + myr.Routes[0].Legs.Count);

            var latLngPoints = new LatLng[(myr.Routes[0].Legs[0].Steps.Count) * 2];
            int index = 0;
            foreach (var step in myr.Routes[0].Legs[0].Steps)
            {
                var startingLat = step.Start_location.Lat;
                var startingLong = step.Start_location.Lng;
                var endingLat = step.End_location.Lat;
                var endingLong = step.End_location.Lng;

                latLngPoints[index++] = new LatLng(startingLat, startingLong);
                latLngPoints[index++] = new LatLng(endingLat, endingLong);
            }

            var polylineoption = new PolylineOptions();
            polylineoption.InvokeColor(Android.Graphics.Color.DarkBlue);
            polylineoption.Geodesic(true);
            polylineoption.Add(latLngPoints);
            g.AddPolyline(polylineoption);
        }

        async void OnButtonClicked(object sender, EventArgs e, Position destination)
        {
            var currentLatitude = 0.0;
            var currentLongitude = 0.0;

            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;

            var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(2000));

            currentLongitude = position.Longitude;
            currentLatitude = position.Latitude;
            var currentPosition = new Position(currentLatitude, currentLongitude);

            DrawRoute(NativeMap, currentPosition, destination);
        }   
    }
}
