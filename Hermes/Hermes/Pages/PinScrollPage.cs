using Hermes.Capability.Map;
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
    public class PinScrollPage : ContentPage
    {
        MapsController Controller;

        public PinScrollPage(MapsController controller)
        {
            Controller = controller;

            Label header = new Label
            {
                Text = "Pins",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center
            };

            var pinTypePicker = new Picker
            {
                Title = "Filter by type",
                ItemsSource = { "Medical", "Shelter", "Supplies" }
            };


            //Dummy Data
            var pinCard = new PinItem
            {
                Address = "4172 Kaitlin Dr Vadnais Heights",
                Information = "Justin's house",
                PinType = "medical"
            };

            var pinCard2 = new PinItem
            {
                Address = "4176 Kaitlin Dr Vadnais Heights",
                Information = "Justin's house",
                PinType = "shelter"
            };

            var pinCard3 = new PinItem
            {
                Address = "4180 Kaitlin Dr Vadnais Heights",
                Information = "Justin's house",
                PinType = "supplies"
            };

            var dbPins = controller.Pins;
            //var dbPins = new ObservableCollection<PinItem>();
            //dbPins.Add(pinCard);
            //dbPins.Add(pinCard2);
            //dbPins.Add(pinCard3);

            var pins = new ObservableCollection<PinCard>();

            ListView listView = new ListView
            {
                ItemTemplate = new DataTemplate(typeof(ImageCell)),
                ItemsSource = pins,
            };

            listView.ItemTemplate.SetBinding(ImageCell.TextProperty, "Address");
            listView.ItemTemplate.SetBinding(ImageCell.DetailProperty, "Info");
            listView.ItemTemplate.SetBinding(ImageCell.ImageSourceProperty, "Image");

            foreach (var p in dbPins)
            {
                if (p.PinType == pinTypePicker.SelectedItem.ToString())
                {
                    var pinC = new PinCard
                    {
                        Address = p.Address,
                        Info = p.Information,
                        Image = p.PinType + ".png",
                        Type = p.PinType
                    };
                    pins.Add(pinC);
                }
            }

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
                    pinTypePicker,
                    listView
                }
            };
        }
    }
}
