using System;
using System.Collections.Generic;
using System.Net;

using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Widget;
using Hermes.Droid;
using Hermes.Models;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.Android;

[assembly: ExportRenderer(typeof(RouteCustomMap), typeof(RouteCustomRenderer))]
namespace Hermes.Droid
{
    class RouteCustomRenderer : MapRenderer, GoogleMap.IInfoWindowAdapter
    {
        List<CustomPin> customPins;

        public RouteCustomRenderer(Context context) : base(context)
        {
            //Example of how to do a toast: 
            //DependencyService.Get<IMessage>().LongAlert("This is a toast!");
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
                var formsMap = (RouteCustomMap)e.NewElement;
                customPins = formsMap.CustomPins;
                Control.GetMapAsync(this);
            }
        }

        protected override void OnMapReady(GoogleMap map)
        {
            base.OnMapReady(map);

            DrawRoute(NativeMap);

            NativeMap.InfoWindowClick += OnInfoWindowClick;
            NativeMap.SetInfoWindowAdapter(this);
        }

        public void DrawRoute(GoogleMap g)
        {
            var lat1 = "42.02332";
            var long1 = "-93.66791";
            var lat2 = "42.020454";
            var long2 = "-93.60969";

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

            Route myr = JsonConvert.DeserializeObject<Route>(json);

            DependencyService.Get<IMessage>().LongAlert("checking how many GWP in root:" + myr.Legs.Count);
/*
            var latLngPoints = new LatLng[myr.Legs.Count];
            int index = 0;
            foreach (var leg in myr.Legs)
            {
                var startingLat = leg.Start_location.Lat;
                var startingLong = leg.Start_location.Lng;
                var endingLat = leg.End_location.Lat;
                var endingLong = leg.End_location.Lng;

                latLngPoints[index++] = new LatLng(startingLat, endingLat);
                latLngPoints[index++] = new LatLng(endingLat, endingLong);
            }

            var polylineoption = new PolylineOptions();
            polylineoption.InvokeColor(Android.Graphics.Color.Green);
            polylineoption.Geodesic(true);
            polylineoption.Add(latLngPoints);
            g.AddPolyline(polylineoption);

            
            DependencyService.Get<IMessage>().LongAlert("How many lines in this JSON: " + json.Split('\n').Length);

            var lstDecodedPoints = FnDecodePolylinePoints(json);
            var latLngPoints = new LatLng[lstDecodedPoints.Count];
            int index = 0;
            foreach (Position loc in lstDecodedPoints)
            {
                latLngPoints[index++] = new LatLng(loc.Latitude, loc.Longitude);
            }

            var polylineoption = new PolylineOptions();
            polylineoption.InvokeColor(Android.Graphics.Color.Green);
            polylineoption.Geodesic(true);
            polylineoption.Add(latLngPoints);

            try
            {
                g.AddPolyline(polylineoption);
            } catch (Exception e)
            {
                if (e.Source != null)
                    Console.WriteLine("Exception source: {0}", e.Source);
                throw;
            }
            */
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
    }
}