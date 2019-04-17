using Hermes.Capability.Map;
using Hermes.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
            try
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
                    Items = { "All","Medical", "Shelter", "Supplies" },
                    SelectedIndex = 0
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

                var cards = new ObservableCollection<PinCard>();

                var dbPins = controller.Pins;
                //var dbPins = new ObservableCollection<PinItem>();
                dbPins.Add(pinCard);
                dbPins.Add(pinCard2);
                dbPins.Add(pinCard3);

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
                        cards.Add(pinC);
                    }
                }

                ListView listView = new ListView
                {
                    ItemTemplate = new DataTemplate(typeof(ImageCell)),
                    ItemsSource = cards,
                };

                listView.ItemTemplate.SetBinding(ImageCell.TextProperty, "Address");
                listView.ItemTemplate.SetBinding(ImageCell.DetailProperty, "Info");
                listView.ItemTemplate.SetBinding(ImageCell.ImageSourceProperty, "Image");

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
            catch(Exception e)
            {
                Debug.WriteLine("Justin suckssss");
                Debug.WriteLine(e.Message);
                Debug.WriteLine(e.StackTrace); 
            }
            
        }
    }
}
