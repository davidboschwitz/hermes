using System;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Hermes
{
    public class PinPage : ContentPage
    {
        Map map;

        public PinPage()
        {
            //Entries for latitude and longitude
            var entryLong = new Entry { Placeholder = "Enter Longitude Here" };
            entryLong.Keyboard = Keyboard.Create(KeyboardFlags.CapitalizeNone);
            entryLong.StyleId = "longitude";

            var entryLat = new Entry { Placeholder = "Enter Latitude Here" };
            entryLat.Keyboard = Keyboard.Create(KeyboardFlags.CapitalizeNone);
            entryLat.StyleId = "latitude";

            var startingLat = 42.025250;
            var startingLong = -93.650870;

            map = new Map
            {
                IsShowingUser = true,
                HeightRequest = 100,
                WidthRequest = 960,
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            map.MoveToRegion(MapSpan.FromCenterAndRadius(
                new Position(startingLat, startingLong), Distance.FromMiles(3))); // Iowa State University

            var position = new Position(startingLat, startingLong);
            var pin = new Pin
            {
                Type = PinType.Place,
                Position = position,
                Label = "Edge Ames",
                Address = "2311 Chamberlain St, Ames, IA 50014"
            };
            map.Pins.Add(pin);


            // create more pins
            var morePins = new Button { Text = "Add pin" };
            morePins.Clicked += (sender, e) => {
                map.Pins.Add(new Pin
                {
                    Position = new Position(Double.Parse(entryLong.Text), Double.Parse(entryLat.Text)),
                    Label = "Custom added pin"
                });
                map.MoveToRegion(MapSpan.FromCenterAndRadius(
                    new Position(Double.Parse(entryLong.Text), Double.Parse(entryLat.Text)), Distance.FromMiles(1.5)));

            };

            //TO-DO
            //Relocate to the button that we just made 
            var reLocate = new Button { Text = "Re-center" };
            reLocate.Clicked += (sender, e) => {
                map.MoveToRegion(MapSpan.FromCenterAndRadius(
                    new Position(startingLat, startingLong), Distance.FromMiles(3)));
            };

            //--------------------------  create the stack layout   ---------------------------------------------

            // create text entries
            var entries = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children =
                {
                    entryLat, entryLong
                }
            };

            // create buttons
            var buttons = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children = {
                    morePins, reLocate
                }
            };

            // put the page together
            Content = new StackLayout
            {
                Spacing = 0,
                Children = {
                    map,
                    entries,
                    buttons
                }
            };


        }
    }
}
