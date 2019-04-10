using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Hermes.Views
{
    public class NewsCell : ViewCell
    {

        public static readonly BindableProperty TitleProperty =
        BindableProperty.Create("Title", typeof(string), typeof(NewsCell), "Title");

        public static readonly BindableProperty BodyProperty =
        BindableProperty.Create("Body", typeof(string), typeof(NewsCell), "Body");

        public NewsCell()
        {
            //instantiate each of our views
            StackLayout cellWrapper = new StackLayout();
            StackLayout horizontalLayout = new StackLayout();
            Label left = new Label();
            Label right = new Label()
            {
                Text = ""
            };

            //set bindings
            left.SetBinding(Label.TextProperty, "Title");

            //Set properties for desired design
            cellWrapper.BackgroundColor = Color.FromHex("#eee");
            horizontalLayout.Orientation = StackOrientation.Horizontal;
            right.HorizontalOptions = LayoutOptions.EndAndExpand;
            left.TextColor = Color.FromHex("#f35e20");

            //add views to the view hierarchy
            horizontalLayout.Children.Add(left);
            cellWrapper.Children.Add(horizontalLayout);
            View = cellWrapper;
        }

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public string Body
        {
            get { return (string)GetValue(BodyProperty); }
            set { SetValue(BodyProperty, value); }
        }

    }
}
