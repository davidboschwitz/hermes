using Hermes.Capability.Map;
using Hermes.Models;
using Hermes.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;


namespace Hermes.Pages
{
    public class PinInfoPage : ContentPage
    {
        protected MainPage RootPage => Application.Current.MainPage as MainPage;

        MapsController Controller;
        private Func<object> clear;

        public PinInfoPage(MapsController controller)
        {
            Controller = controller;

            Label header = new Label
            {
                Text = "Pin Information",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center
            };

            Geocoder geoCoder = new Geocoder();

            Picker pinType = new Picker
            {
                Title = "Pin Type",
                Items = { "Medical", "Supplies", "Shelter" }

            };

            Entry address = new Entry
            {
                Placeholder = "Address: Street, City State"
            };

            Entry information = new Entry { Placeholder = "Information" };

            Entry url = new Entry
            {
                Placeholder = "URL"
            };


            Button submit = new Button
            {
                Text = "Submit"
            };

            submit.Clicked += sub_ClickedAsync;
            
            async void sub_ClickedAsync(object sender, EventArgs e)
            {
                var positions = (await geoCoder.GetPositionsForAddressAsync(address.Text));

                Position position = positions.FirstOrDefault();

                CustomPin newPin = new CustomPin
                {
                    Type = PinType.Place,
                    Position = new Position(position.Latitude, position.Longitude),
                    Address = address.Text,
                    Information = information.Text,
                    Label = pinType.SelectedItem.ToString(),
                    Url = url.Text
                };
                await RootPage.NavigateToPage(new AdminPinPage(newPin, Controller));
            }

            Content = new StackLayout
            {
                Spacing = 1,
                Children = {
                    header,
                    pinType,
                    address,
                    information,
                    url,
                    submit
                }
            };
        }
    }
}
