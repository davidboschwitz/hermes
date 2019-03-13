using Hermes.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Hermes.Pages
{
    class MedicalPinPage : ContentPage
    {
        public MedicalPinPage()
        {
            MedicalPinMap customMap = new MedicalPinMap()
            {
                HeightRequest = 100,
                WidthRequest = 960,
                VerticalOptions = LayoutOptions.FillAndExpand,
                MapType = MapType.Street,
            };

            var examplePinMedical = new CustomPin
            {
                Type = PinType.Place,
                Position = new Position(42.02290, -93.63912),
                Address = " - need to possibly implement - ",
                Id = "medical",
                Label = "medical",
                Url = "http://www.redcross.org"
            };

            customMap.CustomPins = new List<CustomPin> { examplePinMedical};
            customMap.Pins.Add(examplePinMedical);
            customMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(42.025250, -93.650870), Distance.FromMiles(1.0)));

            async void OnAlertYesNoClicked(object sender, EventArgs e)
            {
                var answer = await DisplayAlert("Question?", "Would you like to play a game", "Yes", "No");
                Debug.WriteLine("Answer: " + answer);
            }

            Content = new StackLayout
            {
                Spacing = 0,
                Children = {
                    customMap,

                }
            };
        }
    }
}
