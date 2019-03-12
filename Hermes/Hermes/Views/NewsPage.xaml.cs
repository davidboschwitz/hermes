using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using Plugin.Geolocator;
using Hermes.Models;

namespace Hermes.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewsPage : ContentPage
    {
        private const string newLine = "\r\n";
        private double latitude;
        private double longitude;
        private SearchBar searchBar;

        public NewsPage()
        {
            InitializeComponent();
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

            List<ReportItem> reportList = RetrieveData();

            /*Dummy data*/
            ReportItem a1 = new ReportItem
            {
                Guid = Guid.NewGuid(),
                TimeStamp = DateTime.Now.AddMinutes(12.35),
                From = "Ames PD",
                Title = "Flooding Occuring in Ames",
                Text = "Overnight rainfall of 3 to 5 inches across Story County flooded numerous roadways and triggered mudslides, with authorities responding to reports of people trapped in stranded vehicles and forecasters issuing an elevated flood warning for the region Wednesday morning.",
                LocationBased = false,
                LatLong = null

            };
            ReportItem a2 = new ReportItem
            {
                Guid = Guid.NewGuid(),
                TimeStamp = DateTime.Now,
                From = "Red Cross",
                Title = "Red Cross Giving Aid at Memorial Union",
                Text = "The Red Cross is providing shelter, food, health services and emotional support during this challenging situation to those affected, like Rakiea, Jenna and Ollie, whose stories you can read here.  The Red Cross is working around the clock with our partners to get help to where it’s most needed, and we’re reaching more neighborhoods each day.",
                LocationBased = true,
                LatLong = new Tuple<Double, Double>(42.023267, -93.645772)
            };
            reportList.Add(a1);
            reportList.Add(a2);
            /*End dummy data*/

            for (int i = 0; i < reportList.Count; i++)
            {
                labelArr.Add(new Label()
                {
                    Text = reportList[i].From + newLine + reportList[i].TimeStamp + newLine + reportList[i].Title + newLine + reportList[i].Text + newLine,
                    BackgroundColor = Color.MistyRose
                });
                if (reportList[i].LocationBased == true)
                {
                    labelArr[i].Text += newLine + "Check Coordinates:" + reportList[i].LatLong + newLine;
                    labelArr[i].BackgroundColor = Color.NavajoWhite;
                }
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

        public List<ReportItem> RetrieveData()
        {
            List<ReportItem> reportList = new List<ReportItem>();
            
            return reportList;
        }
    }
}
