using Hermes.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Hermes.Pages
{
    public class PinInfoPage : ContentPage
    {
        public PinInfoPage()
        {
            Label header = new Label
            {
                Text = "Pin Information",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center
            };

            Picker pinType = new Picker
            {
                Title = "Pin Type",
                Items = { "Medical Pin", "Supply Pin", "Shelter Pin" }

            };

            Entry address = new Entry
            {
                Placeholder = "Address"
            };

            Editor information = new Editor { Placeholder = "Information" };

            Entry url = new Entry
            {
                Placeholder = "url"
            };


            Button submit = new Button
            {
                Text = "Submit"
            };

            submit.Clicked += sub_Clicked;

            void sub_Clicked(object sender, EventArgs e)
            {
                CustomPin newPin = new CustomPin
                {
                    Type = PinType.Place,
                    //TODO: Position = new Position(e.Point.Latitude, e.Point.Longitude),
                    Address = address.Text,
                    //TODO: Id = ,
                    Label = pinType.SelectedItem.ToString(),
                    Url = url.Text
                };
            }

            Content = new StackLayout
            {
                Spacing = 0,
                Children = {
                    header,
                    pinType,
                    information,
                    url,
                    submit
                }
            };
        }
               
    }
}
