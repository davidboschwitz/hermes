using Hermes.Capability.Map;
using Hermes.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Hermes.Pages
{
    class PinCardMap : ContentPage
    {
        MapsController Controller;

        public PinCardMap(CustomPin p, MapsController controller)
        {
            Controller = controller;

            Position center = p.Position;

            OriginalCustomMap customMap = new OriginalCustomMap()
            {
                HeightRequest = 100,
                WidthRequest = 960,
                VerticalOptions = LayoutOptions.FillAndExpand,
                MapType = MapType.Street,
            };

            var outputPin = new List<CustomPin>();
            outputPin.Add(p);

            customMap.CustomPins = outputPin;
            customMap.Pins.Add(p);
            
            //Button back = new Button
            //{
            //    Text = "back"
            //};

            //back.Clicked += back_clickedAsync;

            //async void back_clickedAsync(object sender, EventArgs e)
            //{
            //    await Navigation.PopModalAsync();
            //}

            customMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(center.Latitude, center.Longitude), Distance.FromMiles(1.0)));

            Content = new StackLayout
            {
                Spacing = 0,
                Children = {
                    //back,
                    customMap
                }
            };
        }
    }
}
