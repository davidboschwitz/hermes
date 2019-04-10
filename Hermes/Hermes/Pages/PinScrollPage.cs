using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Hermes.Pages
{
    class PinScrollPage : ContentPage
    {
        ImageCell pinCard = new ImageCell
        {
            Text = "Address",
            Detail = "",
            ImageSource = "/Resources/medical.png"
        };

        public PinScrollPage()
        {
            Label header = new Label
            {
                Text = "Pins",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center
            };

            List<ImageCell> pins = new List<ImageCell>();

            //foreach(pin in db){
            //  create Entry for pin
            //  add Entry to pins
            //}



            //List & lt; PinInfo & gt; pinInfo = new List& lt; PinInfo & gt;
            //{
            //    new Person("Abigail", new DateTime(1975, 1, 15), Color.Aqua),
            //new Person("Bob", new DateTime(1976, 2, 20), Color.Black),
            //// ...etc.,...
            //new Person("Yvonne", new DateTime(1987, 1, 10), Color.Purple),
            //new Person("Zachary", new DateTime(1988, 2, 5), Color.Red)
            //};

            ListView listView = new ListView
            {
                ItemsSource = pins,

                // Define template for displaying each item.
                // (Argument of DataTemplate constructor is called for 
                //      each item; it must return a Cell derivative.)

            };

            Content = new StackLayout
            {
                Children = {
                    header,
                    listView
                }
            };


        }
    }
}
