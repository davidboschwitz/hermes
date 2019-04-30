using Hermes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Hermes;


namespace Hermes.Pages
{
    public class PinInfoPage : ContentPage
    {
        public PinInfoPage()
        {
            Label header = new Label
            {
                Text = Hermes.Resources.PinInformation,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center
            };

            Geocoder geoCoder = new Geocoder();

            Picker pinType = new Picker
            {
                Title = Hermes.Resources.PinType,
                Items = { Hermes.Resources.MedicalPin, Hermes.Resources.SupplyPin, Hermes.Resources.ShelterPin }

            };

            Entry address = new Entry
            {
                Placeholder = Hermes.Resources.AddressStreetCityState
            };

             Editor information = new Editor { Placeholder = Hermes.Resources.Information };

            Entry url = new Entry
            {
                Placeholder = Hermes.Resources.URL
            };


            Button submit = new Button
            {
                Text = Hermes.Resources.Submit
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
                    //TODO: Id = ,
                    Label = pinType.SelectedItem.ToString(),
                    Url = url.Text
                };

                await Navigation.PushModalAsync(new AdminPinPage(newPin));
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
