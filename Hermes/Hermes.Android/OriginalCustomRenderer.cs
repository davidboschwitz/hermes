﻿using System;
using System.Collections.Generic;

using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Widget;
using Hermes.Droid;
using Hermes.Models;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.Android;
using Button = Xamarin.Forms.Button;

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

            if (map != null)
            {
                map.MapClick += GoogleMap_MapClick;
            }
        }

        private void GoogleMap_MapClick(object sender, GoogleMap.MapClickEventArgs e)
        {
            ((OriginalCustomMap)Element).OnTap(new Position(e.Point.Latitude, e.Point.Longitude));
            var addingPin = new CustomPin
            {
                Type = PinType.Place,
                Position = new Position(e.Point.Latitude, e.Point.Longitude),
                Address = " - need to possibly implement - ",
                Id = "shelter",
                Label = "shelter",
                Url = "http://www.redcross.org"
            };

            Map.Pins.Add(addingPin);
            customPins.Add(addingPin);
        }

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
    }
}
