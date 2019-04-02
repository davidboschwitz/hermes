using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Hermes.Models;
using Hermes.Capability.News;
using Hermes.Database;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace Hermes.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewsPage : ContentPage
    {
        private const string newLine = "\r\n";
        private SearchBar searchBar;
        private INewsController Controller;

        public NewsPage(INewsController controller)
        {
            InitializeComponent();
            Controller = controller;
            BindingContext = this;
            this.Content = BuildView();
        }

        //void GetLastLocationFromDevice()
        //{
        //    var location = CrossGeolocator.Current;
        //    var position = location.GetPositionAsync();

        //    latitude = position.Result.Latitude;
        //    longitude = position.Result.Longitude;
        //    System.Diagnostics.Debug.WriteLine("Latitude {0}, Longitude {1}", this.latitude, this.longitude);

        //}


        public List<Label> GenerateReports()
        {
            List<Label> labelArr = new List<Label>();

            ObservableCollection<NewsItem> reportList = RetrieveData();

            for (int i = 0; i < reportList.Count; i++)
            {
                labelArr.Add(new Label()
                {
                    Text = reportList[i].SenderID + newLine + reportList[i].CreatedTimestamp + newLine + reportList[i].Title + newLine + reportList[i].Body + newLine,
                    BackgroundColor = Color.MistyRose
                });

            }
        return labelArr;
        }

        public Grid BuildView()
        {
            List<Label> labelArr = GenerateReports();
            Grid grid = new Grid { Padding = 20 };
            ScrollView scroll = new ScrollView { };
            StackLayout stack = new StackLayout { Orientation = StackOrientation.Vertical };
            Label resultsLabel = new Label
            {
                Text = "Result will appear here.",
                VerticalOptions = LayoutOptions.Fill,
                FontSize = 14
            };
            searchBar = new SearchBar()
            {
                Placeholder = "Search here",
                SearchCommand = new Command(() => { resultsLabel.Text = "Result: " + searchBar.Text + " is what you asked for."; })
            };
     

            stack.Children.Add(resultsLabel);
            stack.Children.Add(searchBar);

            grid.Children.Add(scroll);
            scroll.Content = stack;
            for (int i = 0; i < labelArr.Count; i++)
            {
                stack.Children.Add(labelArr[i]);
                //Terrible way to check if need button fix later
                if (labelArr[i].BackgroundColor == Color.NavajoWhite)
                {
                    stack.Children.Add(new Button()
                    {
                        Text = "Click here to see location"
                    });
                }
            }

            return grid;
        }

        public Boolean CheckIfInRange(Tuple<Double, Double> reportLatLong)
        {

            return false;
        }

        public ObservableCollection<NewsItem> RetrieveData()
        {
            ObservableCollection<NewsItem> reportList = Controller.Feed;

            return reportList;
        }

        void Feed_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {

        }

    }
}
