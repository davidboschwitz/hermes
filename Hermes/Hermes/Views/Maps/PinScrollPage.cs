using Hermes.Capability.Map;
using Hermes.Models;
using Hermes.Views;
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
        protected MainPage RootPage => Application.Current.MainPage as MainPage;

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
         
                var cards = new ObservableCollection<PinCard>();

                ObservableCollection<PinItem> dbPins = Controller.Pins;

                foreach (var p in dbPins.ToList())
                {
                        var pinC = new PinCard
                        {
                            Address = p.Address,
                            Info = p.Information,
                            Image = p.PinType.ToString().ToLower() + ".png",
                            Type = p.PinType
                        };
                    Debug.WriteLine(pinC.Image);
                        cards.Add(pinC);               
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
                    var card = e.Item as PinCard;

                    var positions = (await geoCoder.GetPositionsForAddressAsync(card.Address));

                    Position position = positions.FirstOrDefault();

                    CustomPin newPin = new CustomPin
                    {
                        Type = PinType.Place,
                        Position = new Position(position.Latitude, position.Longitude),
                        Address = card.Address,
                        Information = card.Info,
                        Label = card.Type                        
                    };

                    await RootPage.NavigateToPage(new PinCardMap(newPin, Controller));
                }

                listView.RefreshCommand = new Command(refreshAsync);
                Controller.PropertyChanged += (a,b) => { refreshAsync(); };

                pinTypePicker.SelectedIndexChanged += (sender,e) => { refreshAsync(); };

                void refreshAsync()
                {
                    dbPins = Controller.Pins;
                    cards = new ObservableCollection<PinCard>();
                    foreach (var p in dbPins.ToList())
                    {
                        Debug.WriteLine(p.PinType.ToString());
                        Debug.WriteLine(pinTypePicker.SelectedItem);

                        if (p.PinType.ToString() == pinTypePicker.SelectedItem || pinTypePicker.SelectedItem == "All") {
                            Debug.WriteLine(p.PinType.ToString());
                            var pinC = new PinCard
                            {
                                Address = p.Address,
                                Info = p.Information,
                                Image = p.PinType.ToString().ToLower() + ".png",
                                Type = p.PinType
                            };
                            cards.Add(pinC);
                            Debug.WriteLine(pinC.Image);

                        }
                    }

                    listView.ItemsSource = cards;
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
                Debug.WriteLine("-----------ERROR------------");
                Debug.WriteLine(e.Message);
                Debug.WriteLine(e.StackTrace); 
            }
            
        }
    }
}
