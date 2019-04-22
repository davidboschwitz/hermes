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
        public PinCardMap(Position p)
        {
            Position center = p;
            ControlTemplate = (ControlTemplate)Application.Current.Resources["MainPageTemplate"];

            OriginalCustomMap customMap = new OriginalCustomMap()
            {
                HeightRequest = 100,
                WidthRequest = 960,
                VerticalOptions = LayoutOptions.FillAndExpand,
                MapType = MapType.Street,
            };

            Button back = new Button
            {
                Text = "back"
            };

            back.Clicked += back_clickedAsync;

            async void back_clickedAsync(object sender, EventArgs e)
            {
                await Navigation.PopModalAsync();
            }

            customMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(center.Latitude, center.Longitude), Distance.FromMiles(1.0)));

            NavigationPage.SetHasNavigationBar(this, false);

            Content = new StackLayout
            {
                Spacing = 0,
                Children = {
                    back,
                    customMap
                }
            };
        }
    }
}
