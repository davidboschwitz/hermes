using Hermes.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Hermes.Pages
{
    public class PinPlaceSelection : ContentPage
    {
        public PinPlaceSelection()
        {
            Title = "Pin Selection Page";

            Button medical = new Button
            {
                Text = "Medical"

            };

            Button shelter = new Button
            {
                Text = "Shelter"
            };

            Button supplies = new Button
            {
                Text = "Supplies"
            };

            medical.Clicked += but_Clicked;
            shelter.Clicked += but_Clicked;
            supplies.Clicked += but_Clicked;

            Content = new StackLayout{
                Spacing = 0,
                Children = {
                    medical,
                    shelter,
                    supplies
                }
            };
        }

        void but_Clicked(object sender, EventArgs e)
        {
            Button b = sender as Button;
            DependencyService.Get<IMessage>().LongAlert($"Button Clicked: {b.Text}");
            if (b.Text == "Medical")
            {
               this.Navigation.PushAsync(new MedicalPinPage()).ConfigureAwait(false);

            }
            else if (b.Text == "Supplies")
            {
                this.Navigation.PushAsync(new SupplyPinPage()).ConfigureAwait(false);

            }
            else if (b.Text == "Shelter")
            {
                this.Navigation.PushAsync(new ShelterPinPage()).ConfigureAwait(false);

            }
            else {
            }
        }

    }
}
