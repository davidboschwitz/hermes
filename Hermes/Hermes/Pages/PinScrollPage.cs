using Hermes.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Hermes.Pages
{
    class PinScrollPage : ContentPage
    {
        public PinScrollPage()
        {
            Label header = new Label
            {
                Text = "Pins",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center
            };

            var types = new List<String>
            {
                "Medical",
                "Shelter",
                "Supplies",
                "All"
            };

            var typesPicker = new Picker { Title = "Filter by type", TitleColor = Color.Red };
            typesPicker.ItemsSource = types;

            var pinCard = new PinCard
            {
                Address = "4172 Kaitlin Dr Vadnais Heights",
                Info = "Justin's house",
                Image = "medical.png",
                Type = "medical"
            };

            var pins = new ObservableCollection<PinCard>();

            ListView listView = new ListView
            {
                ItemTemplate = new DataTemplate (typeof(ImageCell)),
                ItemsSource = pins,
            };

            listView.ItemTemplate.SetBinding(ImageCell.TextProperty, "Address");
            listView.ItemTemplate.SetBinding(ImageCell.DetailProperty, "Info");
            listView.ItemTemplate.SetBinding(ImageCell.ImageSourceProperty, "Image");

            pins.Add(pinCard);
            pins.Add(pinCard);
            pins.Add(pinCard);
            pins.Add(pinCard);
            pins.Add(pinCard);
            pins.Add(pinCard);
            pins.Add(pinCard);
            pins.Add(pinCard);
            pins.Add(pinCard);
            pins.Add(pinCard);
            pins.Add(pinCard);
            pins.Add(pinCard);
            pins.Add(pinCard);
            pins.Add(pinCard);

            Geocoder geoCoder = new Geocoder();

            listView.ItemTapped += pin_clickedAsync;

            async void pin_clickedAsync(object sender, ItemTappedEventArgs e)
            {
                var location = e.Item as PinCard;

                var positions = (await geoCoder.GetPositionsForAddressAsync(location.Address));

                Position position = positions.FirstOrDefault();

                await Navigation.PushModalAsync(new PinCardMap(position));
            }

            Content = new StackLayout
            {
                Children = {
                    header,
                    typesPicker,
                    listView
                }
            };
        }
    }
}
