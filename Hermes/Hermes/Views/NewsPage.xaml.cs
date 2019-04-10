using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Hermes.Capability.News;
using Hermes.Database;
using System.Collections.Specialized;
using System.Collections.ObjectModel;
using Hermes.Models;

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
            Content = BuildView();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (listView.SelectedItem != null)
            {
                NewsReportPage reportPage = new NewsReportPage();
                reportPage.BindingContext = e.SelectedItem as NewsItem;
                listView.SelectedItem = null;
                await Navigation.PushModalAsync(reportPage);
            }
        }



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

            listView.ItemSelected += OnItemSelected;

            Label resultsLabel = new Label
            {
                Text = "Result will appear here.",
                VerticalOptions = LayoutOptions.Fill,
                FontSize = 14
            };
            searchBar = new NewsSearchBar()
            {
                Placeholder = "Search here",
                SearchCommand = new Command(() => { resultsLabel.Text = "Result: " + searchBar.Text + " is what you asked for."; })
            };
            listView.ItemsSource = Controller.Feed;

            stack.Children.Add(resultsLabel);
            stack.Children.Add(searchBar);
            stack.Children.Add(listView);
            scroll.Content = stack;
            grid.Children.Add(scroll);

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
