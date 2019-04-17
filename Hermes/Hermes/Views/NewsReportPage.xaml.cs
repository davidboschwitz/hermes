using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hermes.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewsReportPage : ContentPage
    {

        public String Body { get; set; }

        public NewsReportPage() {

        var titleLabel = new Label
        {
            FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
            FontAttributes = FontAttributes.Bold
        };
        titleLabel.SetBinding(Label.TextProperty, "Title");

        var bodyLabel = new Label
        {
            FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
            FontAttributes = FontAttributes.Bold
        };
        bodyLabel.SetBinding(Label.TextProperty, "Body");

        var dismissButton = new Button { Text = "Dismiss" };
        dismissButton.Clicked += OnDismissButtonClicked;

        Content = new StackLayout
        {
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            Children = {
                new StackLayout {
                    Orientation = StackOrientation.Horizontal,
                    Children = {
                        new Label {
                            FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                                HorizontalOptions = LayoutOptions.CenterAndExpand
                        },
                        titleLabel
                    }
                },
                new StackLayout {
                    Orientation = StackOrientation.Horizontal,
                    Children = {
                       new Label {
                           FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                           HorizontalOptions = LayoutOptions.CenterAndExpand
                       },
                       bodyLabel
                    }
                },
                dismissButton
            }
        };
    }

        async void OnDismissButtonClicked(object sender, EventArgs args)
        {
            await Navigation.PopModalAsync();
        }
    }
}