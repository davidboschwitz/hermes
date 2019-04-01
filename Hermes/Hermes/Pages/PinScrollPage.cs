using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

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

            List<ImageCell> pins = new List<ImageCell>();

            //foreach(pin in db){
            //  create Entry for pin
            //  add Entry to pins
            //}

            ImageCell pinCard = new ImageCell
            {
                Text = "Address",
                Detail = "",
                ImageSource = "/Resources/medical.png"
            };


            var list = new ListView();
            list.ItemsSource = new[] { pinCard };


            Content = list;

            
            //foreach(var p in pins)
            //{
            //    stack.Children.Add(p);
            //}

        }
    }
}
