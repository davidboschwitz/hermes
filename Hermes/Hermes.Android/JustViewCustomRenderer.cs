using System;
using System.Collections.Generic;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Widget;
using Hermes.Droid;
using Xamarin.Forms;
using Hermes.Models;
using Xamarin.Forms.Maps.Android;
using Xamarin.Forms.Maps;

[assembly: ExportRenderer(typeof(JustViewCustomMap), typeof(OriginalCustomRenderer))]
namespace Hermes.Droid
{
    class JustViewCustomRenderer : MapRenderer, GoogleMap.IInfoWindowAdapter
    {
        List<CustomPin> customPins;

        public JustViewCustomRenderer(Context context) : base(context)
        {
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