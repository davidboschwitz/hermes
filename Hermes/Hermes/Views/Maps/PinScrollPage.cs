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
        readonly MapsController Controller;

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
                    Debug.WriteLine(p.PinType.ToString());
                        var pinC = new PinCard
                        {
                            Address = p.Address,
                            Info = p.Information,
                            //Image = "/" + p.PinType + ".png",
                            Image = "/medical.png",
                            Type = p.PinType
                        };
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
                    var location = e.Item as PinCard;

                    var positions = (await geoCoder.GetPositionsForAddressAsync(location.Address));

                    Position position = positions.FirstOrDefault();

                    await Navigation.PushModalAsync(new PinCardMap(position));
                }

                listView.RefreshCommand = new Command(refreshAsync);
                Controller.PropertyChanged += (a,b) => { refreshAsync(); };

                void refreshAsync()
                {
                    dbPins = Controller.Pins;
                    cards = new ObservableCollection<PinCard>();
                    foreach (var p in dbPins.ToList())
                    {
                        Debug.WriteLine(p.PinType.ToString());
                        var pinC = new PinCard
                        {
                            Address = p.Address,
                            Info = p.Information,
                            //Image = "/" + p.PinType + ".png",
                            Image = "/medical.png",
                            Type = p.PinType
                        };
                        cards.Add(pinC);
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
                Debug.WriteLine("Justin suckssss");
                Debug.WriteLine(e.Message);
                Debug.WriteLine(e.StackTrace); 
            }
            
        }

        void refresh()
        {

        }

        public ObservableCollection<PinItem> RetrieveData()
        {
            ObservableCollection<PinItem> pinList = Controller.Pins;

            return pinList;
        }
    }
}
